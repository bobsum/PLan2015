class EventPointViewModel {
    id: number;
    houseId: number;
    houseName: string;
    amount : KnockoutObservable<number>;

    constructor(point: IEventPointDto) {
        this.id = point.id;
        this.houseId = point.houseId;
        this.houseName = point.houseName;
        this.amount = ko.observable(point.amount);
    }
}

class EventViewModel {
    id: number;
    name: string;
    totalPoints: number;
    points: KnockoutObservableArray<EventPointViewModel>;
    houseNames: string;
    sum: KnockoutComputed<number>;
    isValid: KnockoutComputed<boolean>;
    isExpanded = ko.observable<boolean>();

    toggleExpanded() {
        this.isExpanded(!this.isExpanded());
    }

    constructor(event: IEventDto) {
        this.id = event.id;
        this.name = event.name;
        this.totalPoints = event.totalPoints;
        this.points = ko.observableArray(ko.utils.arrayMap(event.points, p => new EventPointViewModel(p)));
        this.houseNames = ko.utils.arrayMap(event.points, p => p.houseName).join(', ');
        this.sum = ko.computed(() => {
            var sum = 0;
            ko.utils.arrayForEach(this.points(), p => {
                sum += (+p.amount() || 0);
            });
            return sum;
        });
        this.isValid = ko.computed(() => (this.totalPoints === this.sum()));
    }

    sendUpdate(event: EventViewModel) {
        $.ajax({
            url: '/api/event/' + this.id,
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
            url: '/api/event/' + this.id,
            type: 'DELETE'
        });
    }
}

class CreateEventViewModel {
    name = ko.observable<string>();
    totalPoints = ko.observable<number>();
    houseIds = ko.observableArray<number>();
    isValid: KnockoutComputed<boolean>;

    constructor() {
        this.isValid = ko.computed(() => (this.name() && this.totalPoints() && this.houseIds().length > 0));    
    }
}

class EventApp {
    newEvent = ko.observable(new CreateEventViewModel());
    houses = ko.observableArray<IHouseDto>();
    events = ko.observableArray<EventViewModel>();

    add(event: IEventDto) {
        this.events.push(new EventViewModel(event));
    }
    remove(id: number) {
        this.events.remove(e => (e.id === id));
    }
    update(event: IEventDto) {
        var old = ko.utils.arrayFirst(this.events(), e => (e.id === event.id));
        if (old) {
            old.points(ko.utils.arrayMap(event.points, p => new EventPointViewModel(p)));
        }
    }

    sendCreate() {
        var event = this.newEvent();

        $.ajax({
            url: '/api/event',
            type: 'POST',
            data: {
                name: event.name(),
                totalPoints: event.totalPoints(),
                points: ko.utils.arrayMap(event.houseIds(), id => {
                    return {
                        houseId: id,
                        amount: 0
                    }
                })
            }
        });

        this.newEvent(new CreateEventViewModel());
    }
}

$(() => {
    var app = new EventApp();
    var eventHub = $.connection.eventHub;

    ko.applyBindings(app);

    eventHub.client.add = event => {
        app.add(event);
    };

    eventHub.client.update = event => {
        app.update(event);
    };

    eventHub.client.remove = id => {
        app.remove(id);
    };

    $.connection.hub.start();

    $.get('/api/event', events => {
        ko.utils.arrayForEach(events, event => {
            app.add(<IEventDto>event);
        });
    }, 'json');

    $.get('/api/house', houses => {
        app.houses(houses);
    }, 'json');
});