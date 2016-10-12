var Activity;
(function (Activity) {
    var Index;
    (function (Index) {
        var ActivityPointViewModel = (function () {
            function ActivityPointViewModel(point) {
                this.id = point.id;
                this.houseId = point.houseId;
                this.houseName = point.houseName;
                this.amount = ko.observable(point.amount);
                this.visible = ko.observable(point.visible);
            }
            return ActivityPointViewModel;
        }());
        var ActivityViewModel = (function () {
            function ActivityViewModel(activity) {
                var _this = this;
                this.isExpanded = ko.observable();
                this.id = activity.id;
                this.name = activity.name;
                this.totalPoints = activity.totalPoints;
                this.points = ko.observableArray(ko.utils.arrayMap(activity.points, function (p) { return new ActivityPointViewModel(p); }));
                this.houseNames = ko.utils.arrayMap(activity.points, function (p) { return p.houseName; }).join(', ');
                this.sum = ko.computed(function () {
                    var sum = 0;
                    ko.utils.arrayForEach(_this.points(), function (p) {
                        sum += (+p.amount() || 0);
                    });
                    return sum;
                });
                this.allVisible = ko.computed({
                    read: function () {
                        return !ko.utils.arrayFirst(_this.points(), function (p) { return !p.visible(); });
                    },
                    write: function (value) {
                        ko.utils.arrayForEach(_this.points(), function (p) { return p.visible(value); });
                    }
                });
                this.isValid = ko.computed(function () { return (_this.totalPoints >= _this.sum()); });
            }
            ActivityViewModel.prototype.toggleExpanded = function () {
                this.isExpanded(!this.isExpanded());
            };
            ActivityViewModel.prototype.convertPoints = function () {
                var sum = 0;
                ko.utils.arrayForEach(this.points(), function (p) {
                    sum += (+p.amount() || 0);
                });
                var total = this.totalPoints;
                if (sum === 0)
                    return;
                var rate = total / sum;
                ko.utils.arrayForEach(this.points(), function (p) {
                    p.amount(Math.round(p.amount() * rate));
                });
            };
            ActivityViewModel.prototype.sendUpdate = function (activity) {
                $.ajax({
                    url: '/Api/Activity/' + this.id,
                    type: 'PUT',
                    data: {
                        id: this.id,
                        name: this.name,
                        totalPoints: this.totalPoints,
                        points: ko.utils.arrayMap(this.points(), function (p) {
                            return {
                                id: p.id,
                                houseId: p.houseId,
                                houseName: p.houseName,
                                amount: p.amount(),
                                visible: p.visible()
                            };
                        })
                    }
                });
                this.isExpanded(false);
            };
            ActivityViewModel.prototype.sendDelete = function () {
                if (!confirm("Er du sikker på du vil slette begivenheden?"))
                    return;
                $.ajax({
                    url: '/Api/Activity/' + this.id,
                    type: 'DELETE'
                });
            };
            return ActivityViewModel;
        }());
        var CreateActivityViewModel = (function () {
            function CreateActivityViewModel() {
                var _this = this;
                this.name = ko.observable();
                this.totalPoints = ko.observable();
                this.houseIds = ko.observableArray();
                this.isValid = ko.computed(function () { return (_this.name() && _this.totalPoints() && _this.houseIds().length > 0); });
            }
            return CreateActivityViewModel;
        }());
        var App = (function () {
            function App() {
                var _this = this;
                this.newActivity = ko.observable(new CreateActivityViewModel());
                this.houses = ko.observableArray();
                this.activities = ko.observableArray();
                var hub = $.connection.activityHub;
                hub.client.add = function (activity) {
                    _this.add(activity);
                };
                hub.client.update = function (activity) {
                    _this.update(activity);
                };
                hub.client.remove = function (id) {
                    _this.remove(id);
                };
                $.connection.hub.start();
                $.get('/Api/Activity', function (activities) {
                    ko.utils.arrayForEach(activities, function (activity) {
                        _this.add(activity);
                    });
                }, 'json');
                $.get('/Api/House', function (houses) {
                    _this.houses(houses);
                }, 'json');
            }
            App.prototype.add = function (activity) {
                this.activities.push(new ActivityViewModel(activity));
            };
            App.prototype.remove = function (id) {
                this.activities.remove(function (a) { return (a.id === id); });
            };
            App.prototype.update = function (activity) {
                var old = ko.utils.arrayFirst(this.activities(), function (l) { return (l.id === activity.id); });
                if (old) {
                    old.points(ko.utils.arrayMap(activity.points, function (p) { return new ActivityPointViewModel(p); }));
                }
            };
            App.prototype.sendCreate = function () {
                var activity = this.newActivity();
                $.ajax({
                    url: '/Api/Activity',
                    type: 'POST',
                    data: {
                        name: activity.name(),
                        totalPoints: activity.totalPoints(),
                        points: ko.utils.arrayMap(activity.houseIds(), function (id) {
                            return {
                                houseId: id,
                                amount: 0
                            };
                        })
                    }
                });
                this.newActivity(new CreateActivityViewModel());
            };
            return App;
        }());
        Index.App = App;
    })(Index = Activity.Index || (Activity.Index = {}));
})(Activity || (Activity = {}));
var MagicGames;
(function (MagicGames) {
    var Marker;
    (function (Marker) {
        var StatusViewModel = (function () {
            function StatusViewModel(name) {
                this.name = name;
                this.progress = ko.observable(0);
            }
            return StatusViewModel;
        }());
        var UploadViewModel = (function () {
            function UploadViewModel() {
                var _this = this;
                this.files = ko.observableArray();
                this.status = ko.observableArray();
                this.selectFile = function (a, e) {
                    var fileList = e.target.files;
                    var files = [];
                    for (var i = 0; i < fileList.length; i++) {
                        files.push(fileList[i]);
                    }
                    _this.files(files);
                    _this.status([]);
                };
                this.isValid = ko.computed(function () { return !!_this.files(); });
            }
            UploadViewModel.prototype.sendUplaod = function () {
                this.status(ko.utils.arrayMap(this.files(), function (file) {
                    var status = new StatusViewModel(file.name);
                    Helpers.readText(file)
                        .progress(function (p) { return status.progress(p / 2); })
                        .done(function (d) {
                        $.ajax({
                            url: '/Api/MagicGamesMarkerSwipe',
                            type: 'POST',
                            data: {
                                name: file.name,
                                data: d
                            }
                        }).done(function () { return status.progress(100); });
                    });
                    return status;
                }));
                this.files(null);
            };
            return UploadViewModel;
        }());
        var App = (function () {
            function App() {
                this.upload = ko.observable(new UploadViewModel());
            }
            return App;
        }());
        Marker.App = App;
    })(Marker = MagicGames.Marker || (MagicGames.Marker = {}));
})(MagicGames || (MagicGames = {}));
var MagicGames;
(function (MagicGames) {
    var Setup;
    (function (Setup) {
        var IntervalViewModel = (function () {
            function IntervalViewModel(interval) {
                this.scoutId = interval.scoutId;
                this.scoutName = interval.scoutName;
                this.amount = ko.observable(interval.amount);
            }
            return IntervalViewModel;
        }());
        var SetupViewModel = (function () {
            function SetupViewModel(setup) {
                var _this = this;
                this.isSaved = ko.observable(true);
                this.intervals = ko.observableArray();
                this.availableIntervals = ko.computed(function () {
                    var intervals = _this.intervals();
                    var unused = _this.getIntervals(intervals.length);
                    ko.utils.arrayForEach(intervals, function (i) {
                        var index = unused.indexOf(i.amount());
                        if (index !== -1) {
                            unused.splice(index, 1);
                        }
                        else {
                            i.amount(0);
                        }
                    });
                    return unused;
                });
                this.next = ko.computed(function () {
                    var next = _this.availableIntervals()[0];
                    return next ? 'Næste: ' + next + ' min' : 'Alle intervaller er fordelt.';
                });
                this.setInterval = function (interval) {
                    interval.amount(!interval.amount() ? _this.availableIntervals()[0] : 0);
                    _this.isSaved(false);
                };
                this.isExpanded = ko.observable();
                this.distribution = ko.computed(function () { return ko.utils.arrayMap(_this.intervals(), function (i) { return '(' + i.scoutName + ' = ' + (i.amount() || '?') + ' min)'; }).join(', '); });
                this.houseId = setup.houseId;
                this.houseName = setup.houseName;
                this.intervals(ko.utils.arrayMap(setup.intervals, function (interval) {
                    return new IntervalViewModel(interval);
                }));
            }
            SetupViewModel.prototype.sendSave = function () {
                $.ajax({
                    url: '/Api/MagicGamesSetup/' + this.houseId,
                    type: 'PUT',
                    data: {
                        houseId: this.houseId,
                        houseName: this.houseName,
                        intervals: ko.utils.arrayMap(this.intervals(), function (i) {
                            return {
                                scoutId: i.scoutId,
                                scoutName: i.scoutName,
                                amount: i.amount()
                            };
                        })
                    }
                });
            };
            SetupViewModel.prototype.update = function (setup) {
                this.intervals(ko.utils.arrayMap(setup.intervals, function (interval) {
                    return new IntervalViewModel(interval);
                }));
                this.isSaved(true);
            };
            SetupViewModel.prototype.toggleExpanded = function () {
                this.isExpanded(!this.isExpanded());
            };
            SetupViewModel.prototype.getIntervals = function (quantity) {
                var priority = [5, 10, 15, 20, 30];
                var intervals = [];
                for (var i = 0; i < quantity; i++) {
                    intervals.push(priority[i % priority.length]);
                }
                intervals.sort(Helpers.compare);
                return intervals;
            };
            return SetupViewModel;
        }());
        var App = (function () {
            function App() {
                var _this = this;
                this.setups = ko.observableArray();
                var hub = $.connection.magicGamesSetupHub;
                hub.client.update = function (dto) {
                    var house = ko.utils.arrayFirst(_this.setups(), function (h) { return (h.houseId === dto.houseId); });
                    house.update(dto);
                };
                $.connection.hub.start();
                $.get('/Api/MagicGamesSetup', function (setups) {
                    _this.setups(ko.utils.arrayMap(setups, function (setup) {
                        return new SetupViewModel(setup);
                    }));
                }, 'json');
            }
            return App;
        }());
        Setup.App = App;
    })(Setup = MagicGames.Setup || (MagicGames.Setup = {}));
})(MagicGames || (MagicGames = {}));
var MagicGames;
(function (MagicGames) {
    var Score;
    (function (Score) {
        var App = (function () {
            function App() {
                var _this = this;
                this.scores = ko.observableArray();
                $.get('/Api/MagicGamesScore', function (scores) {
                    _this.scores(scores);
                }, 'json');
            }
            return App;
        }());
        Score.App = App;
    })(Score = MagicGames.Score || (MagicGames.Score = {}));
})(MagicGames || (MagicGames = {}));
var Turnout;
(function (Turnout) {
    var Index;
    (function (Index) {
        var StatusViewModel = (function () {
            function StatusViewModel(name) {
                this.name = name;
                this.progress = ko.observable(0);
            }
            return StatusViewModel;
        }());
        var UploadViewModel = (function () {
            function UploadViewModel() {
                var _this = this;
                this.files = ko.observableArray();
                this.status = ko.observableArray();
                this.selectFile = function (a, e) {
                    var fileList = e.target.files;
                    var files = [];
                    for (var i = 0; i < fileList.length; i++) {
                        files.push(fileList[i]);
                    }
                    _this.files(files);
                    _this.status([]);
                };
                this.isValid = ko.computed(function () { return !!_this.files(); });
            }
            UploadViewModel.prototype.sendUplaod = function () {
                this.status(ko.utils.arrayMap(this.files(), function (file) {
                    var status = new StatusViewModel(file.name);
                    Helpers.readText(file)
                        .progress(function (p) { return status.progress(p / 2); })
                        .done(function (d) {
                        $.ajax({
                            url: '/Api/TurnoutSwipe',
                            type: 'POST',
                            data: {
                                name: file.name,
                                data: d
                            }
                        }).done(function () { return status.progress(100); });
                    });
                    return status;
                }));
                this.files(null);
            };
            return UploadViewModel;
        }());
        var App = (function () {
            function App() {
                this.upload = ko.observable(new UploadViewModel());
            }
            return App;
        }());
        Index.App = App;
    })(Index = Turnout.Index || (Turnout.Index = {}));
})(Turnout || (Turnout = {}));
var Punctuality;
(function (Punctuality) {
    var Index;
    (function (Index) {
        var PunctualityViewModel = (function () {
            function PunctualityViewModel(punctuality) {
                this.id = punctuality.id;
                this.name = punctuality.name;
                this.start = moment(punctuality.start);
                this.stop = moment(punctuality.stop);
                this.stationId = punctuality.stationId;
                this.stationName = punctuality.stationName;
                this.all = punctuality.all;
            }
            PunctualityViewModel.prototype.sendDelete = function () {
                if (!confirm("Er du sikker på du vil slette punktlighed? Alle evt. points slettes også."))
                    return;
                $.ajax({
                    url: '/Api/Punctuality/' + this.id,
                    type: 'DELETE'
                });
            };
            return PunctualityViewModel;
        }());
        var CreatePunctualityViewModel = (function () {
            function CreatePunctualityViewModel() {
                var _this = this;
                this.name = ko.observable();
                this.startDate = ko.observable('');
                this.startTime = ko.observable('');
                this.start = ko.computed(function () {
                    if (_this.startDate().length < 10 || _this.startTime().length < 5)
                        return null;
                    return moment(_this.startDate() + 'T' + _this.startTime());
                });
                this.stopDate = ko.observable('');
                this.stopTime = ko.observable('');
                this.stop = ko.computed(function () {
                    if (_this.stopDate().length < 10 || _this.stopTime().length < 5)
                        return null;
                    return moment(_this.stopDate() + 'T' + _this.stopTime());
                });
                this.stationId = ko.observable();
                this.all = ko.observable(false);
                this.isValid = ko.computed(function () { return !!_this.stationId() && !!_this.name() && !!_this.start() && !!_this.stop() && _this.start().isBefore(_this.stop()); });
            }
            return CreatePunctualityViewModel;
        }());
        var App = (function () {
            function App() {
                var _this = this;
                this.newPunctuality = ko.observable(new CreatePunctualityViewModel());
                this.punctualities = ko.observableArray();
                this.stations = ko.observableArray();
                this.sending = ko.observable(false);
                var hub = $.connection.punctualityHub;
                hub.client.add = function (punctuality) {
                    _this.add(punctuality);
                };
                hub.client.remove = function (id) {
                    _this.remove(id);
                };
                $.connection.hub.start();
                $.get('/Api/Punctuality', function (punctualities) {
                    ko.utils.arrayForEach(punctualities, function (punctuality) {
                        _this.add(punctuality);
                    });
                }, 'json');
                $.get('/Api/PunctualityStation', function (stations) {
                    _this.stations(stations);
                }, 'json');
            }
            App.prototype.sendCreate = function () {
                var _this = this;
                var punctuality = this.newPunctuality();
                this.sending(true);
                $.ajax({
                    url: '/Api/Punctuality',
                    type: 'POST',
                    data: {
                        name: punctuality.name(),
                        start: punctuality.start().toJSON(),
                        stop: punctuality.stop().toJSON(),
                        stationId: punctuality.stationId(),
                        all: punctuality.all()
                    }
                })
                    .done(function () {
                    _this.newPunctuality(new CreatePunctualityViewModel());
                })
                    .always(function () { return _this.sending(false); });
            };
            App.prototype.add = function (punctuality) {
                this.punctualities.push(new PunctualityViewModel(punctuality));
                this.punctualities.sort(function (a, b) {
                    if (a.start < b.start)
                        return -1;
                    if (a.start > b.start)
                        return 1;
                    return 0;
                });
            };
            App.prototype.remove = function (id) {
                this.punctualities.remove(function (p) { return (p.id === id); });
            };
            return App;
        }());
        Index.App = App;
    })(Index = Punctuality.Index || (Punctuality.Index = {}));
})(Punctuality || (Punctuality = {}));
var Punctuality;
(function (Punctuality) {
    var Station;
    (function (Station) {
        var HouseStatusViewModel = (function () {
            function HouseStatusViewModel(house) {
                this.name = house.name;
                this.scouts = house.scouts;
                this.arrived = ko.utils.arrayFilter(house.scouts, function (s) { return s.arrived; }).length;
            }
            return HouseStatusViewModel;
        }());
        var PunctualityViewModel = (function () {
            function PunctualityViewModel(punctuality) {
                this.id = punctuality.id;
                this.name = punctuality.name;
                this.start = moment(punctuality.start);
                this.stop = moment(punctuality.stop);
                this.stationId = punctuality.stationId;
                this.stationName = punctuality.stationName;
                this.all = punctuality.all;
            }
            return PunctualityViewModel;
        }());
        var App = (function () {
            function App(id) {
                var _this = this;
                this.id = id;
                this.punctuality = ko.observable();
                this.punctualities = ko.observableArray();
                this.houses = ko.observableArray();
                this.rfid = new Helpers.RfidReader(function (t) { return !!_this.punctuality() ? _this.sendSwipe(t) : null; });
                var timeout = 15 * 1000;
                this.hub = $.connection.punctualityHub;
                this.hub.client.add = function (punctuality) {
                    _this.add(punctuality);
                };
                this.hub.client.remove = function (punctualityId) {
                    _this.remove(punctualityId);
                };
                this.hub.client.updatedStatus = function (houses) {
                    _this.houses(ko.utils.arrayMap(houses, function (h) { return new HouseStatusViewModel(h); }));
                };
                $.connection.hub.start()
                    .done(function () {
                    $.get('/Api/Punctuality', function (punctualities) { return _this.addAll(punctualities); }, 'json')
                        .done(function () { return setInterval(function () { return _this.findCurrent(); }, timeout); });
                });
                this.all = ko.computed(function () {
                    var punctuality = _this.punctuality();
                    return !!punctuality ? punctuality.all : false;
                });
            }
            App.prototype.sendSwipe = function (rfid) {
                $.ajax({
                    url: '/Api/PunctualitySwipe',
                    type: 'POST',
                    data: {
                        punctualityId: this.punctuality().id,
                        rfid: rfid
                    }
                });
            };
            App.prototype.addAll = function (punctualities) {
                var _this = this;
                ko.utils.arrayForEach(punctualities, function (punctuality) {
                    _this.add(punctuality);
                });
            };
            App.prototype.add = function (punctuality) {
                this.punctualities.push(new PunctualityViewModel(punctuality));
                this.findCurrent();
            };
            App.prototype.remove = function (id) {
                this.punctualities.remove(function (p) { return (p.id === id); });
                this.findCurrent();
            };
            App.prototype.findCurrent = function () {
                var _this = this;
                var now = moment();
                var oldPunctuality = this.punctuality.peek();
                var newPunctuality = ko.utils.arrayFirst(this.punctualities(), function (p) { return p.stationId === _this.id && now.isBetween(p.start, p.stop); });
                if (oldPunctuality !== newPunctuality) {
                    this.punctuality(newPunctuality);
                    this.houses(null);
                    this.hub.server.setId(!!newPunctuality ? newPunctuality.id : null, !!oldPunctuality ? oldPunctuality.id : null);
                }
            };
            return App;
        }());
        Station.App = App;
    })(Station = Punctuality.Station || (Punctuality.Station = {}));
})(Punctuality || (Punctuality = {}));
var Quiz;
(function (Quiz) {
    var Index;
    (function (Index) {
        var App = (function () {
            function App() {
                var _this = this;
                this.rfid = new Helpers.RfidReader(function (t) { return _this.sendSwipe(t); });
                this.question = ko.observable();
                this.hasFocus = ko.observable();
                this.swipes = ko.observableArray();
                $.get('/Api/TeamMember', function (teamMembers) {
                    _this.teamMembers = teamMembers;
                }, 'json');
            }
            App.prototype.nextQuestion = function () {
                var _this = this;
                this.swipes.removeAll();
                this.hasFocus(false);
                $.ajax({
                    url: '/Api/QuizQuestion',
                    type: 'POST',
                    data: {}
                })
                    .done(function (question) { return _this.question(question); });
            };
            App.prototype.sendSwipe = function (rfid) {
                if (!!ko.utils.arrayFirst(this.teamMembers, function (t) { return t.rfid === rfid; })) {
                    this.nextQuestion();
                    return;
                }
                var question = this.question();
                if (!question)
                    return;
                this.swipes.push(rfid);
                $.ajax({
                    url: '/Api/QuizSwipe',
                    type: 'POST',
                    data: {
                        rfid: rfid,
                        questionId: question.id
                    }
                });
            };
            return App;
        }());
        Index.App = App;
    })(Index = Quiz.Index || (Quiz.Index = {}));
})(Quiz || (Quiz = {}));
var Score;
(function (Score) {
    var Index;
    (function (Index) {
        var ScoreHouseViewModel = (function () {
            function ScoreHouseViewModel(name, visibleAmount, hiddenAmount) {
                this.name = name;
                this.visibleAmount = visibleAmount;
                this.hiddenAmount = hiddenAmount;
            }
            return ScoreHouseViewModel;
        }());
        var ScoreSchoolViewModel = (function () {
            function ScoreSchoolViewModel(name, houses) {
                this.name = name;
                var sumH = 0;
                var sumV = 0;
                this.houses = ko.utils.arrayMap(houses, function (house) {
                    sumV += house.amount;
                    sumH += house.hiddenAmount;
                    return new ScoreHouseViewModel(house.name, house.amount, house.hiddenAmount);
                }).sort(function (a, b) {
                    return b.visibleAmount - a.visibleAmount;
                });
                this.visibleAmount = sumV;
                this.hiddenAmount = sumH;
            }
            return ScoreSchoolViewModel;
        }());
        var App = (function () {
            function App() {
                var _this = this;
                this.schools = ko.observableArray();
                var hub = $.connection.scoreHub;
                hub.client.updated = function (schools) {
                    _this.schools(ko.utils.arrayMap(schools, function (school) {
                        return new ScoreSchoolViewModel(school.name, school.houses);
                    }).sort(function (a, b) {
                        return b.visibleAmount - a.visibleAmount;
                    }));
                };
                $.connection.hub.start();
            }
            return App;
        }());
        Index.App = App;
    })(Index = Score.Index || (Score.Index = {}));
})(Score || (Score = {}));
var SortingHat;
(function (SortingHat) {
    var Index;
    (function (Index) {
        var App = (function () {
            function App() {
                var _this = this;
                this.scout = ko.observable();
                this.scouts = ko.observableArray();
                this.rfid = new Helpers.RfidReader(function (t) { return _this.findScout(t); });
                $.get('/Api/Scout', function (scouts) {
                    _this.scouts(scouts);
                }, 'json');
            }
            App.prototype.findScout = function (tag) {
                this.scout(ko.utils.arrayFirst(this.scouts(), function (s) { return s.rfid === tag; }));
            };
            return App;
        }());
        Index.App = App;
    })(Index = SortingHat.Index || (SortingHat.Index = {}));
})(SortingHat || (SortingHat = {}));
var Helpers;
(function (Helpers) {
    var RfidReader = (function () {
        function RfidReader(callback) {
            this.callback = callback;
            this.buffer = '';
        }
        RfidReader.prototype.resetBuffer = function () {
            this.buffer = '';
        };
        RfidReader.prototype.resetBufferTimer = function () {
            var _this = this;
            if (this.bufferTimer)
                clearTimeout(this.bufferTimer);
            this.bufferTimer = setTimeout(function () { return _this.resetBuffer(); }, 50);
        };
        RfidReader.prototype.keydown = function (event) {
            if (event.repeat)
                return;
            var key = event.key;
            if (/^\w$/.test(key)) {
                this.buffer += key;
                this.resetBufferTimer();
            }
            else if (key === 'Enter' && this.buffer.length) {
                this.callback(this.buffer);
                this.resetBuffer();
            }
        };
        return RfidReader;
    }());
    Helpers.RfidReader = RfidReader;
    function readText(file) {
        var reader = new FileReader();
        var deferred = $.Deferred();
        reader.onload = function (e) { return deferred.resolve(e.target.result); };
        reader.onprogress = function (e) { return deferred.notify(e.loaded * 100 / e.total); };
        reader.onerror = function (e) { return deferred.reject(e); };
        reader.readAsText(file);
        return deferred.promise();
    }
    Helpers.readText = readText;
    function compare(a, b) {
        return (a - b);
    }
    Helpers.compare = compare;
})(Helpers || (Helpers = {}));
//# sourceMappingURL=App.js.map