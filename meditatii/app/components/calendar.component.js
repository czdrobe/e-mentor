"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var moment = require("moment");
var _ = require("underscore");
var CustomCalendarComponent = /** @class */ (function () {
    function CustomCalendarComponent() {
        this.currentDate = moment();
        this.dayNames = ['S', 'M', 'T', 'W', 'T', 'F', 'S'];
        this.weeks = [];
        this.sortedDates = [];
        this.selectedDates = [];
        this.onSelectDate = new core_1.EventEmitter();
        moment.locale('de');
    }
    CustomCalendarComponent.prototype.ngOnInit = function () {
        moment.locale('de');
        this.generateCalendar();
    };
    CustomCalendarComponent.prototype.ngOnChanges = function (changes) {
        if (changes.selectedDates &&
            changes.selectedDates.currentValue &&
            changes.selectedDates.currentValue.length > 1) {
            // sort on date changes for better performance when range checking
            this.sortedDates = _.sortBy(changes.selectedDates.currentValue, function (m) { return m.mDate.valueOf(); });
            this.generateCalendar();
        }
    };
    // date checkers
    CustomCalendarComponent.prototype.isToday = function (date) {
        return moment().isSame(moment(date), 'day');
    };
    CustomCalendarComponent.prototype.isSelected = function (date) {
        return _.findIndex(this.selectedDates, function (selectedDate) {
            return moment(date).isSame(selectedDate.mDate, 'day');
        }) > -1;
    };
    CustomCalendarComponent.prototype.isSelectedMonth = function (date) {
        return moment(date).isSame(this.currentDate, 'month');
    };
    CustomCalendarComponent.prototype.selectDate = function (date) {
        this.onSelectDate.emit(date);
    };
    // actions from calendar
    CustomCalendarComponent.prototype.prevMonth = function () {
        this.currentDate = moment(this.currentDate).subtract(1, 'months');
        this.generateCalendar();
    };
    CustomCalendarComponent.prototype.nextMonth = function () {
        this.currentDate = moment(this.currentDate).add(1, 'months');
        this.generateCalendar();
    };
    CustomCalendarComponent.prototype.firstMonth = function () {
        this.currentDate = moment(this.currentDate).startOf('year');
        this.generateCalendar();
    };
    CustomCalendarComponent.prototype.lastMonth = function () {
        this.currentDate = moment(this.currentDate).endOf('year');
        this.generateCalendar();
    };
    CustomCalendarComponent.prototype.prevYear = function () {
        this.currentDate = moment(this.currentDate).subtract(1, 'year');
        this.generateCalendar();
    };
    CustomCalendarComponent.prototype.nextYear = function () {
        this.currentDate = moment(this.currentDate).add(1, 'year');
        this.generateCalendar();
    };
    // generate the calendar grid
    CustomCalendarComponent.prototype.generateCalendar = function () {
        var dates = this.fillDates(this.currentDate);
        var weeks = [];
        while (dates.length > 0) {
            weeks.push(dates.splice(0, 7));
        }
        this.weeks = weeks;
    };
    CustomCalendarComponent.prototype.fillDates = function (currentMoment) {
        var _this = this;
        var firstOfMonth = moment(currentMoment).startOf('month').day();
        var firstDayOfGrid = moment(currentMoment).startOf('month').subtract(firstOfMonth, 'days');
        var start = firstDayOfGrid.date();
        return _.range(start, start + 42)
            .map(function (date) {
            var d = moment(firstDayOfGrid).date(date);
            return {
                today: _this.isToday(d),
                selected: _this.isSelected(d),
                mDate: d,
            };
        });
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Array)
    ], CustomCalendarComponent.prototype, "selectedDates", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", Object)
    ], CustomCalendarComponent.prototype, "onSelectDate", void 0);
    CustomCalendarComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'yoshimi-calendar',
            templateUrl: 'calendar.component.html',
            providers: []
        }),
        __metadata("design:paramtypes", [])
    ], CustomCalendarComponent);
    return CustomCalendarComponent;
}());
exports.CustomCalendarComponent = CustomCalendarComponent;
//# sourceMappingURL=calendar.component.js.map