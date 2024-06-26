import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { MemberEditComponent } from '../member-edit/member-edit.component';
import { ConfirmService } from '../_services/confirm.service';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {

  constructor(private confirmService: ConfirmService) {}

  canDeactivate(component: MemberEditComponent):  Observable<boolean> | boolean {
    if(component.editForm.dirty)
    {
      // return confirm("Esti sigur ca vrei sa continui? Toate datele nesalvate vor fi pierdute.");

      return this.confirmService.confirm();
    }

    return true;
  }
  
}
