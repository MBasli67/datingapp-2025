import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { APIError } from '../../../types/error';

@Component({
  selector: 'app-server-error',
  imports: [],
  templateUrl: './server-error.html',
  styleUrl: './server-error.css',
})

export class ServerError {


private router = inject(Router);
// protected error = signal<APIError | null>(null);
protected error : APIError;
protected showDetail: boolean = false;

constructor (){
  const navigation = this.router.currentNavigation();
  this.error = navigation?.extras?.state?.['error'];
}

detailsToggle(){
  this.showDetail = !this.showDetail;
}

}
