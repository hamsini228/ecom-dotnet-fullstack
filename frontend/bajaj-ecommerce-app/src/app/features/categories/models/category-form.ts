import { FormGroup,FormControl,Validators } from "@angular/forms";
export class CategoryForm {
    categoryForm =new FormGroup({
        categoryName: new FormControl('',[Validators.required ,Validators.minLength(3),Validators.maxLength(20)]),
        description :new FormControl('', [Validators.required ,Validators.maxLength(200)])
    });
}
