import { Teacher } from "./teacher.type";
import { Category } from "./categories.type";
import { Cycle } from "./cycle.type";

export interface Ad {
    Id:number,
    Code:string,
    Duration:number,
    Price:number,
    Added:Date,
    Cycles:Cycle[],
    Description:string,
    Category:Category,
    Teacher:Teacher
}