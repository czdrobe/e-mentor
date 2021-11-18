"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var FilterByMateriaPipe = /** @class */ (function () {
    function FilterByMateriaPipe() {
    }
    FilterByMateriaPipe.prototype.transform = function (categories, materia) {
        if (categories) {
            if (materia !== undefined && materia != "") {
                var lowermateria = materia.toLowerCase();
                var filteredItems = categories.filter(function (listing) { return listing.Name.toLowerCase().indexOf(lowermateria) > -1; }).splice(0, 10);
                return filteredItems;
                //return categories.filter((listing: Category) => listing.Name.toLowerCase().indexOf(lowermateria) > -1);
            }
            /*else
            {
                var filteredItems = categories.splice(0,10);
                return filteredItems;
            }*/
        }
    };
    FilterByMateriaPipe = __decorate([
        core_1.Pipe({ name: 'filterByMateria' })
    ], FilterByMateriaPipe);
    return FilterByMateriaPipe;
}());
exports.FilterByMateriaPipe = FilterByMateriaPipe;
//# sourceMappingURL=listfilter.pip.js.map