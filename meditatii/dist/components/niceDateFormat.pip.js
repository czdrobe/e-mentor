"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var NiceDateFormatPipe = /** @class */ (function () {
    function NiceDateFormatPipe() {
    }
    NiceDateFormatPipe.prototype.transform = function (value) {
        var valueDate = new Date(value);
        var _value = valueDate.getTime();
        var dif = Math.floor(((Date.now() - _value) / 1000) / 86400);
        if (dif < 30) {
            return convertToNiceDate(value);
        }
        else {
            var datePipe = new common_1.DatePipe("en-US");
            value = datePipe.transform(value, 'MMM-dd-yyyy');
            return value;
        }
    };
    NiceDateFormatPipe = __decorate([
        core_1.Pipe({ name: 'niceDateFormatPipe' })
    ], NiceDateFormatPipe);
    return NiceDateFormatPipe;
}());
exports.NiceDateFormatPipe = NiceDateFormatPipe;
function convertToNiceDate(time) {
    var date = new Date(time), diff = (((new Date()).getTime() - date.getTime()) / 1000), daydiff = Math.floor(diff / 86400);
    if (isNaN(daydiff) || daydiff < 0 || daydiff >= 31)
        return '';
    return daydiff == 0 && (diff < 60 && "Chiar acum" ||
        diff < 120 && "1 minut in urmă" ||
        diff < 3600 && Math.floor(diff / 60) + " minute in urmă" ||
        diff < 7200 && "1 ora in urmă" ||
        diff < 86400 && Math.floor(diff / 3600) + " ore in urmă") ||
        daydiff == 1 && "Ieri" ||
        daydiff < 7 && daydiff + " zile in urmă" ||
        daydiff < 31 && Math.ceil(daydiff / 7) + " saptamani in urmă";
}
//# sourceMappingURL=niceDateFormat.pip.js.map