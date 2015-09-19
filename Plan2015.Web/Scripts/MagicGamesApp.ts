module MagicGames {
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
        
        setInterval(interval: IntervalViewModel) {
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

        distribution = ko.computed(() => ko.utils.arrayMap(this.intervals(), i => '(' + i.scoutName + ' = ' + (i.amount() || '?') + ' min)' ).join(', '));
    }

    export class App {
        houses = ko.observableArray<HouseViewModel>();
        constructor() {
            var hub = $.connection.magicGamesHub;

            hub.client.update = dto => {
                var house = ko.utils.arrayFirst(this.houses(), h => (h.houseId === dto.houseId));
                house.update(dto);
                console.log(dto);
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