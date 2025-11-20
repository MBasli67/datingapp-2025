import { Component, inject, signal } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { Router, RouterLink, RouterLinkActive } from "@angular/router";
import { ToastService } from '../../core/services/toast-service';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav {
  //Inject services
  protected accountService = inject(AccountService);
  private router =  inject(Router);
  private toast = inject(ToastService);



  protected creds: any = {};

  login()
  {
          this.accountService.login(this.creds).
          subscribe(
            {
            next: result => {
              // console.log(result),
              //On a successful login go to members component
              this.router.navigateByUrl('/members');
              this.toast.success('Logged in successfully');
              this.creds = {};
            },
            error: error => {
              this.toast.error(error.error);
            }
            }
        );
  }

  logOut(){
    this.accountService.logout();
    //After logout got to home component
    this.router.navigateByUrl('/');
  }

}
