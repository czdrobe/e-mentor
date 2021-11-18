import { Pipe, PipeTransform } from '@angular/core';
import { Category } from '../types/categories.type';
@Pipe({name: 'filterByMateria'})
export class FilterByMateriaPipe implements PipeTransform {

    transform(categories : any, materia: string): any[] {
        if (categories) {
            if (materia !== undefined && materia != "")
            {
                var lowermateria = materia.toLowerCase();
                var filteredItems = categories.filter((listing: Category) => listing.Name.toLowerCase().indexOf(lowermateria) > -1).splice(0, 10);
                return filteredItems;
                //return categories.filter((listing: Category) => listing.Name.toLowerCase().indexOf(lowermateria) > -1);
            }
            /*else
            {
                var filteredItems = categories.splice(0,10);
                return filteredItems;
            }*/
        }
    }
}