var MagicGames;
(function (MagicGames) {
    function compare(a, b) {
        return (a - b);
    }
    function getIntervals(quantity) {
        var priority = [5, 10, 15, 20, 30];
        var intervals = [];
        for (var i = 0; i < quantity; i++) {
            intervals.push(priority[i % priority.length]);
        }
        intervals.sort(compare);
        return intervals;
    }
    var IntervalViewModel = (function () {
        function IntervalViewModel(interval) {
            this.scoutId = interval.scoutId;
            this.scoutName = interval.scoutName;
            this.amount = ko.observable(interval.amount);
        }
        return IntervalViewModel;
    })();
    var HouseViewModel = (function () {
        function HouseViewModel(house) {
            var _this = this;
            this.isSaved = ko.observable(true);
            this.intervals = ko.observableArray();
            this.availableIntervals = ko.computed(function () {
                var intervals = _this.intervals();
                var unused = getIntervals(intervals.length);
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
            this.isExpanded = ko.observable();
            this.distribution = ko.computed(function () { return ko.utils.arrayMap(_this.intervals(), function (i) { return '(' + i.scoutName + ' = ' + (i.amount() || '?') + ' min)'; }).join(', '); });
            this.houseId = house.houseId;
            this.houseName = house.houseName;
            this.intervals(ko.utils.arrayMap(house.intervals, function (interval) {
                return new IntervalViewModel(interval);
            }));
        }
        HouseViewModel.prototype.setInterval = function (interval) {
            interval.amount(!interval.amount() ? this.availableIntervals()[0] : 0);
            this.isSaved(false);
        };
        HouseViewModel.prototype.sendSave = function () {
            $.ajax({
                url: '/api/magicgames/' + this.houseId,
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
        HouseViewModel.prototype.update = function (house) {
            this.intervals(ko.utils.arrayMap(house.intervals, function (interval) {
                return new IntervalViewModel(interval);
            }));
            this.isSaved(true);
        };
        HouseViewModel.prototype.toggleExpanded = function () {
            this.isExpanded(!this.isExpanded());
        };
        return HouseViewModel;
    })();
    var App = (function () {
        function App() {
            var _this = this;
            this.houses = ko.observableArray();
            var hub = $.connection.magicGamesHub;
            hub.client.update = function (dto) {
                var house = ko.utils.arrayFirst(_this.houses(), function (h) { return (h.houseId === dto.houseId); });
                house.update(dto);
                console.log(dto);
            };
            $.connection.hub.start();
            $.get('/api/magicgames', function (houses) {
                _this.houses(ko.utils.arrayMap(houses, function (house) {
                    return new HouseViewModel(house);
                }));
            }, 'json');
        }
        return App;
    })();
    MagicGames.App = App;
})(MagicGames || (MagicGames = {}));
//# sourceMappingURL=MagicGamesApp.js.map