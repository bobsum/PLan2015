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
            }
            return ActivityPointViewModel;
        })();
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
                this.isValid = ko.computed(function () { return (_this.totalPoints === _this.sum()); });
            }
            ActivityViewModel.prototype.toggleExpanded = function () {
                this.isExpanded(!this.isExpanded());
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
                                amount: p.amount()
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
        })();
        var CreateActivityViewModel = (function () {
            function CreateActivityViewModel() {
                var _this = this;
                this.name = ko.observable();
                this.totalPoints = ko.observable();
                this.houseIds = ko.observableArray();
                this.isValid = ko.computed(function () { return (_this.name() && _this.totalPoints() && _this.houseIds().length > 0); });
            }
            return CreateActivityViewModel;
        })();
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
                this.activities.remove(function (e) { return (e.id === id); });
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
        })();
        Index.App = App;
    })(Index = Activity.Index || (Activity.Index = {}));
})(Activity || (Activity = {}));
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
        })();
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
                    console.log('click');
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
        })();
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
        })();
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
        })();
        Score.App = App;
    })(Score = MagicGames.Score || (MagicGames.Score = {}));
})(MagicGames || (MagicGames = {}));
var Turnout;
(function (Turnout) {
    var Index;
    (function (Index) {
        var App = (function () {
            function App() {
                var _this = this;
                this.files = ko.observableArray();
                this.selectFile = function (a, e) {
                    var fileList = e.target.files;
                    var files = [];
                    for (var i = 0; i < fileList.length; i++) {
                        files.push(fileList[i]);
                    }
                    _this.files(files);
                };
                this.isValid = ko.computed(function () { return !!_this.files(); });
            }
            App.prototype.sendUplaod = function () {
                ko.utils.arrayForEach(this.files(), function (file) {
                    Helpers.readText(file).done(function (d) {
                        $.ajax({
                            url: '/Api/TurnoutSwipe',
                            type: 'POST',
                            data: {
                                name: file.name,
                                data: d
                            }
                        });
                    });
                });
                this.files(null);
            };
            return App;
        })();
        Index.App = App;
    })(Index = Turnout.Index || (Turnout.Index = {}));
})(Turnout || (Turnout = {}));
var Helpers;
(function (Helpers) {
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