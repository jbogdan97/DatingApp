export interface User
{
    username: string;
    token: string;
    photoUrl?: string;
    knownAs?: string;
    gender?: string;
    roles: string[];
}

// let data: number | string = 42;
// data = "10";

// interface Car {
//     color: string;
//     model: string;
//     topSpeed?: number; // ? -> optional in cazul asta, permite null cred
// }

// const car1: Car = 
// {
//     color: "blue",
//     model: 'BMW'
// }

// const car2: Car = 
// {
//     color: 'red',
//     model: 'Mercedes',
//     topSpeed: 100
// }

// const multiply = (x: number, y: number) => {
//     return x*y; // fara return recunoaste automat ca trebuie sa returneze void adica nimic
// }