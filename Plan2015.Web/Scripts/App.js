var Lesson;
(function (Lesson) {
    var Index;
    (function (Index) {
        var LessonPointViewModel = (function () {
            function LessonPointViewModel(point) {
                this.id = point.id;
                this.houseId = point.houseId;
                this.houseName = point.houseName;
                this.amount = ko.observable(point.amount);
            }
            return LessonPointViewModel;
        })();
        var LessonViewModel = (function () {
            function LessonViewModel(lesson) {
                var _this = this;
                this.isExpanded = ko.observable();
                this.id = lesson.id;
                this.name = lesson.name;
                this.totalPoints = lesson.totalPoints;
                this.points = ko.observableArray(ko.utils.arrayMap(lesson.points, function (p) { return new LessonPointViewModel(p); }));
                this.houseNames = ko.utils.arrayMap(lesson.points, function (p) { return p.houseName; }).join(', ');
                this.sum = ko.computed(function () {
                    var sum = 0;
                    ko.utils.arrayForEach(_this.points(), function (p) {
                        sum += (+p.amount() || 0);
                    });
                    return sum;
                });
                this.isValid = ko.computed(function () { return (_this.totalPoints === _this.sum()); });
            }
            LessonViewModel.prototype.toggleExpanded = function () {
                this.isExpanded(!this.isExpanded());
            };
            LessonViewModel.prototype.sendUpdate = function (lesson) {
                $.ajax({
                    url: '/Api/Lesson/' + this.id,
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
            LessonViewModel.prototype.sendDelete = function () {
                if (!confirm("Er du sikker på du vil slette begivenheden?"))
                    return;
                $.ajax({
                    url: '/Api/Lesson/' + this.id,
                    type: 'DELETE'
                });
            };
            return LessonViewModel;
        })();
        var CreateLessonViewModel = (function () {
            function CreateLessonViewModel() {
                var _this = this;
                this.name = ko.observable();
                this.totalPoints = ko.observable();
                this.houseIds = ko.observableArray();
                this.isValid = ko.computed(function () { return (_this.name() && _this.totalPoints() && _this.houseIds().length > 0); });
            }
            return CreateLessonViewModel;
        })();
        var App = (function () {
            function App() {
                var _this = this;
                this.newLesson = ko.observable(new CreateLessonViewModel());
                this.houses = ko.observableArray();
                this.lessons = ko.observableArray();
                var hub = $.connection.lessonHub;
                hub.client.add = function (lesson) {
                    _this.add(lesson);
                };
                hub.client.update = function (lesson) {
                    _this.update(lesson);
                };
                hub.client.remove = function (id) {
                    _this.remove(id);
                };
                $.connection.hub.start();
                $.get('/Api/Lesson', function (lessons) {
                    ko.utils.arrayForEach(lessons, function (lesson) {
                        _this.add(lesson);
                    });
                }, 'json');
                $.get('/Api/House', function (houses) {
                    _this.houses(houses);
                }, 'json');
            }
            App.prototype.add = function (lesson) {
                this.lessons.push(new LessonViewModel(lesson));
            };
            App.prototype.remove = function (id) {
                this.lessons.remove(function (e) { return (e.id === id); });
            };
            App.prototype.update = function (lesson) {
                var old = ko.utils.arrayFirst(this.lessons(), function (l) { return (l.id === lesson.id); });
                if (old) {
                    old.points(ko.utils.arrayMap(lesson.points, function (p) { return new LessonPointViewModel(p); }));
                }
            };
            App.prototype.sendCreate = function () {
                var lesson = this.newLesson();
                $.ajax({
                    url: '/Api/Lesson',
                    type: 'POST',
                    data: {
                        name: lesson.name(),
                        totalPoints: lesson.totalPoints(),
                        points: ko.utils.arrayMap(lesson.houseIds(), function (id) {
                            return {
                                houseId: id,
                                amount: 0
                            };
                        })
                    }
                });
                this.newLesson(new CreateLessonViewModel());
            };
            return App;
        })();
        Index.App = App;
    })(Index = Lesson.Index || (Lesson.Index = {}));
})(Lesson || (Lesson = {}));
var MagicGames;
(function (MagicGames) {
    var Setup;
    (function (Setup) {
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
                this.setInterval = function (interval) {
                    console.log('click');
                    interval.amount(!interval.amount() ? _this.availableIntervals()[0] : 0);
                    _this.isSaved(false);
                };
                this.isExpanded = ko.observable();
                this.distribution = ko.computed(function () { return ko.utils.arrayMap(_this.intervals(), function (i) { return '(' + i.scoutName + ' = ' + (i.amount() || '?') + ' min)'; }).join(', '); });
                this.houseId = house.houseId;
                this.houseName = house.houseName;
                this.intervals(ko.utils.arrayMap(house.intervals, function (interval) {
                    return new IntervalViewModel(interval);
                }));
            }
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
                this.houses = ko.observableArray();
                $.get('/api/magicgamesscore', function (houses) {
                    _this.houses(houses);
                }, 'json');
            }
            return App;
        })();
        Score.App = App;
    })(Score = MagicGames.Score || (MagicGames.Score = {}));
})(MagicGames || (MagicGames = {}));
//# sourceMappingURL=App.js.map