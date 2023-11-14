import { Category } from "./categories.type";
import { Occupation } from "./occupation.type";
import { Experience } from "./experience.type";

export interface Teacher {
    Id: number,
    FirstName: string,
    LastName: string,
    email: string,
    description: string,
    occupation:Occupation,
    experience: Experience,
    UserCode:string
}