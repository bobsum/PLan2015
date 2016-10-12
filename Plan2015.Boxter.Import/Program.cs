using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using Plan2015.Data;
using Plan2015.Data.Entities;

namespace Plan2015.Boxter.Import
{
    class Program
    {
        private const int DELAY = 3*1000;
        private const string URL = "http://boxter-v42.azurewebsites.net/umbraco/api/boxcall/getall";

        private static readonly ILog _log;
        private static readonly bool _isErrorEnabled;
        private static readonly bool _isWarnEnabled;
        private static readonly bool _isInfoEnabled;

        static Program()
        {
            XmlConfigurator.Configure();

            _log = LogManager.GetLogger("Main");

            _isErrorEnabled = _log.IsErrorEnabled;
            _isWarnEnabled = _log.IsWarnEnabled;
            _isInfoEnabled = _log.IsInfoEnabled;
        }

        static void Main(string[] args)
        {
            Task.WaitAll(Run());
        }

        private static async Task Run()
        {
            int last;
            using (var db = new DataContext())
            {
                last = await db.BoxterSwipes
                    .Select(bs => bs.SwipeId)
                    .DefaultIfEmpty(0)
                    .MaxAsync();
            }
            while (true)
            {
                try
                {
                    Thread.Sleep(DELAY);
                    using (var httpClient = new HttpClient())
                    {
                        var json = await httpClient.GetStringAsync(URL);

                        var dtos = JsonConvert.DeserializeObject<BoxterImportDto[]>(json)
                            .Where(bi => bi.Id > last)
                            .OrderBy(bi => bi.Id)
                            .ToList();

                        if (!dtos.Any()) continue;

                        bool tournoutPointAdded = false;
                        using (var db = new DataContext())
                        {
                            foreach (var dto in dtos)
                            {
                                var rfid = dto.Tag.PadLeft(10, '0');
                                var scout = await db.Scouts.FirstOrDefaultAsync(s => s.Rfid == rfid);

                                if (scout == null)
                                {
                                    if (_isWarnEnabled) _log.Warn($"Rfid not found: {rfid}");
                                    continue;
                                }

                                var swipe = new BoxterSwipe
                                {
                                    SwipeId = dto.Id,
                                    Scout = scout,
                                    BoxId = dto.BoxId,
                                    BoxIdFriendly = dto.BoxIdFriendly,
                                    AppMode = dto.AppMode,
                                    AppResponse = dto.AppResponse,
                                    CreateDate = dto.CreateDate
                                };
                                db.BoxterSwipes.Add(swipe);

                                if (dto.AppMode.Equals("Turnout", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    tournoutPointAdded = true;
                                    var point = new TurnoutPoint
                                    {
                                        Amount = int.Parse(dto.AppResponse),
                                        HouseId = scout.HouseId,
                                        Time = dto.CreateDate
                                    };

                                    db.TurnoutPoints.Add(point);
                                }
                                await db.SaveChangesAsync();

                                last = swipe.SwipeId;
                            }
                        }

                        if (tournoutPointAdded) await httpClient.PostAsync("http://localhost/Api/Turnout/", null);
                        _log.Info("Got new data from Boxter");
                    }
                }
                catch (Exception ex)
                {
                    if (_isErrorEnabled) _log.Error(ex);
                }
            }
        }
    }

    internal class BoxterImportDto
    {
        public int Id { get; set; }
        public string BoxId { get; set; }
        public string BoxIdFriendly { get; set; }
        public string Tag { get; set; }
        public string Hex { get; set; }
        public string AppMode { get; set; }
        public string AppResponse { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
