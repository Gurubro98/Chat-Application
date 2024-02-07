import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const jwtHelper = new JwtHelperService();
  const token = localStorage.getItem('token');
  if (token) {
    return true;
  }
  router.navigate(['login']);
  return false;
};
