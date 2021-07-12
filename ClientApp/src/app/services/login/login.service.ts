import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  get isLoggedIn(): boolean {
    return JSON.parse(localStorage['loggedIn']) as boolean;
  }
  set isLoggedIn(val: boolean) {
    localStorage['loggedIn'] = val;
  }

  get jwtToken(): string {
    return localStorage['jwtToken'];
  }

  set jwtToken(val: string) {
    localStorage['jwtToken'] = val;
  }

  constructor(private router: Router) { 
  }
  
  login(password: string) {
    // TODO: implement this shit lol
    this.isLoggedIn = true;
  }
  
  logout() {
    this.isLoggedIn = false;
    this.router.navigate(['login'])
  }
}
