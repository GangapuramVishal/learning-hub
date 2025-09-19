// var x = 1;
// a();
// b();
// console.log(x);

// function a(){
//     var x = 10;
//     console.log(x);
// }

// function b() {
//     var x = 100;
//     console.log(x);
// }

// function x(callback){
//     console.log("this is function x")
//     callback();
// }

// x(function y(){
//     console.log("this is callback function")
// });

function attachEventListeners(){
    let count = 0;
    document.getElementById("clickme").addEventListener("click", function(){
        console.log("Button clicked and callback function is exicuted", ++count)
    })  
}

let para = document.getElementById("text"); para.addEventListener("mouseover", function(){
    para.style.color = "red";
});

attachEventListeners();

const arr = [1,2,3,4,5,6,7] 

// const result = arr.reduce(function(total, present){
//     total = total + present;
//     return total;
// });

// console.log(result);

// function findmax(arr){
//     let max = 0;
//     for(let i = 0;i < arr.lenght; i ++){
//         if(arr[i] > max){
//             max = arr[i];
//         }
//     }
//     return max;
// }

// console.log(findmax(arr));

// const output = arr.reduce(function(max, current){
//     if(current > max){
//         max = current;
//     }
//     return max;
// }, 0);

// console.log(output);

// const users = [
//     {firstname: "vishal", lastname: "Bhai", age: 20},
//     {firstname: "Bobby", lastname: "Lucifer", age: 20},
//     {firstname: "Meow", lastname: "Chuuu", age: 20},
//     {firstname: "Wild", lastname: "Life", age: 20},
//     {firstname: "Dosa", lastname: "pindi", age: 20}
// ];

// const result = users.map(x => x.firstname + " " + x.lastname);
// console.log(result)

const fruits = ["apple", "banana", "mango", "cherry"];
for (const fruit of fruits){
    console.log(fruit);
}

const string = "Hello World";
for (const char of string){
    console.log(char);
}

const sum = (a,b) => a + b;
console.log(sum(2,3));