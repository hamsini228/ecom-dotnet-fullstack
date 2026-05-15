import { Component, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Role } from '../../models/role';
import { CommonModule } from '@angular/common';
import { RoleService } from '../../services/role-service';
@Component({
  selector: 'bajaj-roles-list',
  imports: [CommonModule],
  templateUrl: './roles-list.html',
  styleUrl: './roles-list.css',
})
export class RolesList {
  roles:Observable<Role[]>;
  private _RoleService =inject(RoleService);
  title:string="Welcome to Role list";
  ngOnInit(): void {
      this.roles=this._RoleService.getAllRoles();
  } 

}
