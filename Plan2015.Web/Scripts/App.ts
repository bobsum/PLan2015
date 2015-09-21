module Lesson.Index {
    class LessonPointViewModel {
        id: number;
        houseId: number;
        houseName: string;
        amount: KnockoutObservable<number>;

        constructor(point: ILessonPointDto) {
            this.id = point.id;
            this.houseId = point.houseId;
            this.houseName = point.houseName;
            this.amount = ko.observable(point.amount);
        }
    }

    class LessonViewModel {
        id: number;
        name: string;
        totalPoints: number;
        points: KnockoutObservableArray<LessonPointViewModel>;
        houseNames: string;
        sum: KnockoutComputed<number>;
        isValid: KnockoutComputed<boolean>;
        isExpanded = ko.observable<boolean>();

        toggleExpanded() {
            this.isExpanded(!this.isExpanded());
        }

        constructor(lesson: ILessonDto) {
            this.id = lesson.id;
            this.name = lesson.name;
            this.totalPoints = lesson.totalPoints;
            this.points = ko.observableArray(ko.utils.arrayMap(lesson.points, p => new LessonPointViewModel(p)));
            this.houseNames = ko.utils.arrayMap(lesson.points, p => p.houseName).join(', ');
            this.sum = ko.computed(() => {
                var sum = 0;
                ko.utils.arrayForEach(this.points(), p => {
                    sum += (+p.amount() || 0);
                });
                return sum;
            });
            this.isValid = ko.computed(() => (this.totalPoints === this.sum()));
        }

        sendUpdate(lesson: LessonViewModel) {
            $.ajax({
                url: '/Api/Lesson/' + this.id,
                type: 'PUT',
                data: {
                    id: this.id,
                    name: this.name,
                    totalPoints: this.totalPoints,
                    points: ko.utils.arrayMap(this.points(), p => {
                        return {
                            id: p.id,
                            houseId: p.houseId,
                            houseName: p.houseName,
                            amount: p.amount()
                        }
                    })
                }
            });

            this.isExpanded(false);
        }

        sendDelete() {
            if (!confirm("Er du sikker på du vil slette begivenheden?")) return;

            $.ajax({
                url: '/Api/Lesson/' + this.id,
                type: 'DELETE'
            });
        }
    }

    class CreateLessonViewModel {
        name = ko.observable<string>();
        totalPoints = ko.observable<number>();
        houseIds = ko.observableArray<number>();
        isValid: KnockoutComputed<boolean>;

        constructor() {
            this.isValid = ko.computed(() => (this.name() && this.totalPoints() && this.houseIds().length > 0));
        }
    }

    export class App {
        newLesson = ko.observable(new CreateLessonViewModel());
        houses = ko.observableArray<IHouseDto>();
        lessons = ko.observableArray<LessonViewModel>();

        add(lesson: ILessonDto) {
            this.lessons.push(new LessonViewModel(lesson));
        }

        remove(id: number) {
            this.lessons.remove(e => (e.id === id));
        }

        update(lesson: ILessonDto) {
            var old = ko.utils.arrayFirst(this.lessons(), l => (l.id === lesson.id));
            if (old) {
                old.points(ko.utils.arrayMap(lesson.points, p => new LessonPointViewModel(p)));
            }
        }

        sendCreate() {
            var lesson = this.newLesson();

            $.ajax({
                url: '/Api/Lesson',
                type: 'POST',
                data: {
                    name: lesson.name(),
                    totalPoints: lesson.totalPoints(),
                    points: ko.utils.arrayMap(lesson.houseIds(), id => {
                        return {
                            houseId: id,
                            amount: 0
                        }
                    })
                }
            });

            this.newLesson(new CreateLessonViewModel());
        }

        constructor() {
            var hub = $.connection.lessonHub;
            
            hub.client.add = lesson => {
                this.add(lesson);
            };

            hub.client.update = lesson => {
                this.update(lesson);
            };

            hub.client.remove = id => {
                this.remove(id);
            };

            $.connection.hub.start();

            $.get('/Api/Lesson', lessons => {
                ko.utils.arrayForEach(lessons, lesson => {
                    this.add(<ILessonDto>lesson);
                });
            }, 'json');

            $.get('/Api/House', houses => {
                this.houses(houses);
            }, 'json');
        }
    }
}


module MagicGames.Setup {
    function compare(a: any, b: any): number {
        return (a - b);
    }

    function getIntervals(quantity): number[] {
        var priority = [5, 10, 15, 20, 30];
        var intervals = [];
        for (var i = 0; i < quantity; i++) {
            intervals.push(priority[i % priority.length]);
        }
        intervals.sort(compare);
        return intervals;
    }

    class IntervalViewModel {
        scoutId: number;
        scoutName: string;
        amount: KnockoutObservable<number>;

        constructor(interval: IMagicGamesIntervalDto) {
            this.scoutId = interval.scoutId;
            this.scoutName = interval.scoutName;
            this.amount = ko.observable(interval.amount);
        }
    }

    class HouseViewModel {
        houseId: number;
        houseName: string;
        isSaved = ko.observable<boolean>(true);

        constructor(house: IMagicGamesHouseDto) {
            this.houseId = house.houseId;
            this.houseName = house.houseName;
            this.intervals(ko.utils.arrayMap(house.intervals, interval => {
                return new IntervalViewModel(interval);
            }));
        }

        intervals = ko.observableArray<IntervalViewModel>();

        availableIntervals = ko.computed<number[]>(() => {
            var intervals = this.intervals();
            var unused = getIntervals(intervals.length);

            ko.utils.arrayForEach(intervals, i => {
                var index = unused.indexOf(i.amount());
                if (index !== -1) {
                    unused.splice(index, 1);
                } else {
                    i.amount(0);
                }
            });

            return unused;
        });

        next = ko.computed<string>(() => {
            var next = this.availableIntervals()[0];
            return next ? 'Næste: ' + next + ' min' : 'Alle intervaller er fordelt.';
        });

        setInterval = (interval: IntervalViewModel) => {
            console.log('click');
            interval.amount(!interval.amount() ? this.availableIntervals()[0] : 0);
            this.isSaved(false);
        }

        sendSave() {
            $.ajax({
                url: '/api/magicgames/' + this.houseId,
                type: 'PUT',
                data: {
                    houseId: this.houseId,
                    houseName: this.houseName,
                    intervals: ko.utils.arrayMap(this.intervals(), i => {
                        return {
                            scoutId: i.scoutId,
                            scoutName: i.scoutName,
                            amount: i.amount()
                        }
                    })
                }
            });
        }

        update(house: IMagicGamesHouseDto) {
            this.intervals(ko.utils.arrayMap(house.intervals, interval => {
                return new IntervalViewModel(interval);
            }));
            this.isSaved(true);
        }

        isExpanded = ko.observable<boolean>();

        toggleExpanded() {
            this.isExpanded(!this.isExpanded());
        }

        distribution = ko.computed(() => ko.utils.arrayMap(this.intervals(), i => '(' + i.scoutName + ' = ' + (i.amount() || '?') + ' min)').join(', '));
    }

    export class App {
        houses = ko.observableArray<HouseViewModel>();
        constructor() {
            var hub = $.connection.magicGamesHub;

            hub.client.update = dto => {
                var house = ko.utils.arrayFirst(this.houses(), h => (h.houseId === dto.houseId));
                house.update(dto);
            };

            $.connection.hub.start();

            $.get('/api/magicgames', houses => {
                this.houses(ko.utils.arrayMap(houses, house => {
                    return new HouseViewModel(<IMagicGamesHouseDto>house);
                }));
            }, 'json');
        }
    }
}

module MagicGames.Score {
    export class App {
        houses = ko.observableArray<IMagicGamesScoreDto>();

        constructor() {
            $.get('/api/magicgamesscore', houses => {
                this.houses(houses);
            }, 'json');
        }
    }
}