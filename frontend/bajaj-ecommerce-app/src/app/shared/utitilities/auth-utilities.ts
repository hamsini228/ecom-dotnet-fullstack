function setAuthInformation(email:string,token:string,role:string,userId:string):void{
    localStorage.setItem('email',email);
    localStorage.setItem('token',token);
    localStorage.setItem('role',role);
    localStorage.setItem('userId',userId);
}

function getToken() :string|null{
return localStorage.getItem('token');
}

function getEmail() :string|null{
    return localStorage.getItem('email');
}

function getRole() :string|null{
    return localStorage.getItem('role');
}
function getUserId():string|null{
    return localStorage.getItem('userId');
}
function removeAuthInformation():void{
    localStorage.clear();
}
export{setAuthInformation , getToken , getEmail , getRole,removeAuthInformation, getUserId}