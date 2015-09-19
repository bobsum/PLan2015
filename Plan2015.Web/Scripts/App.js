var EventPointViewModel = (function () {
    function EventPointViewModel(point) {
        this.id = point.id;
        this.houseId = point.houseId;
        this.houseName = point.houseName;
        this.amount = ko.observable(point.amount);
    }
    return EventPointViewModel;
})();
var EventViewModel = (function () {
    function EventViewModel(event) {
        var _this = this;
        this.isExpanded = ko.observable();
        this.id = event.id;
        this.name = event.name;
        this.totalPoints = event.totalPoints;
        this.points = ko.observableArray(ko.utils.arrayMap(event.points, function (p) { return new EventPointViewModel(p); }));
        this.houseNames = ko.utils.arrayMap(event.points, function (p) { return p.houseName; }).join(', ');
        this.sum = ko.computed(function () {
            var sum = 0;
            ko.utils.arrayForEach(_this.points(), function (p) {
                sum += (+p.amount() || 0);
            });
            return sum;
        });
        this.isValid = ko.computed(function () { return (_this.totalPoints === _this.sum()); });
    }
    EventViewModel.prototype.toggleExpanded = function () {
        this.isExpanded(!this.isExpanded());
    };
    EventViewModel.prototype.sendUpdate = function (event) {
        $.ajax({
            url: '/api/event/' + this.id,
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
    EventViewModel.prototype.sendDelete = function () {
        if (!confirm("Er du sikker på du vil slette begivenheden?"))
            return;
        $.ajax({
            url: '/api/event/' + this.id,
            type: 'DELETE'
        });
    };
    return EventViewModel;
})();
var CreateEventViewModel = (function () {
    function CreateEventViewModel() {
        var _this = this;
        this.name = ko.observable();
        this.totalPoints = ko.observable();
        this.houseIds = ko.observableArray();
        this.isValid = ko.computed(function () { return (_this.name() && _this.totalPoints() && _this.houseIds().length > 0); });
    }
    return CreateEventViewModel;
})();
var EventApp = (function () {
    function EventApp() {
        this.newEvent = ko.observable(new CreateEventViewModel());
        this.houses = ko.observableArray();
        this.events = ko.observableArray();
    }
    EventApp.prototype.add = function (event) {
        this.events.push(new EventViewModel(event));
    };
    EventApp.prototype.remove = function (id) {
        this.events.remove(function (e) { return (e.id === id); });
    };
    EventApp.prototype.update = function (event) {
        var old = ko.utils.arrayFirst(this.events(), function (e) { return (e.id === event.id); });
        if (old) {
            old.points(ko.utils.arrayMap(event.points, function (p) { return new EventPointViewModel(p); }));
        }
    };
    EventApp.prototype.sendCreate = function () {
        var event = this.newEvent();
        $.ajax({
            url: '/api/event',
            type: 'POST',
            data: {
                name: event.name(),
                totalPoints: event.totalPoints(),
                points: ko.utils.arrayMap(event.houseIds(), function (id) {
                    return {
                        houseId: id,
                        amount: 0
                    };
                })
            }
        });
        this.newEvent(new CreateEventViewModel());
    };
    return EventApp;
})();
$(function () {
    var app = new EventApp();
    var eventHub = $.connection.eventHub;
    ko.applyBindings(app);
    eventHub.client.add = function (event) {
        app.add(event);
    };
    eventHub.client.update = function (event) {
        app.update(event);
    };
    eventHub.client.remove = function (id) {
        app.remove(id);
    };
    $.connection.hub.start();
    $.get('/api/event', function (events) {
        ko.utils.arrayForEach(events, function (event) {
            app.add(event);
        });
    }, 'json');
    $.get('/api/house', function (houses) {
        app.houses(houses);
    }, 'json');
});
//# sourceMappingURL=App.js.map