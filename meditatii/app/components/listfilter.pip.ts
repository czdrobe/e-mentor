import { Pipe, PipeTransform } from '@angular/core';
import { Category } from '../types/categories.type';
@Pipe({name: 'filterByMateria'})
export class FilterByMateriaPipe implements PipeTransform {

    transform(categories : any, materia: string): any[] {
        if (categories) {
            if (materia != "")
            {
                var lowermateria = materia.toLowerCase();
                return categories.filter((listing: Category) => listing.Name.toLowerCase().indexOf(lowermateria) > -1);
            }
            else
            {
                return categories;
            }
        }
    }
}