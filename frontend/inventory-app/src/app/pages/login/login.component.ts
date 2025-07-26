import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UsersService } from '../../services/users.service';
import { LoginPayload, LoginResponse } from '../../models/auth.model';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Component({
   selector: 'app-login',
   standalone: true,
   imports: [CommonModule, RouterModule, FormsModule, HttpClientModule],
   templateUrl: './login.component.html',
})
export class LoginComponent {
   email = '';
   password = '';
   constructor(
      private api: UsersService,
      private auth: AuthService,
      private router: Router
   ) {}

   onLogin() {
      const credentials: LoginPayload = {
         email: this.email,
         password: this.password,
      };

      this.api.login(credentials).subscribe({
         next: (res) => {
            this.auth.setToken(res.token);
            this.router.navigate(['/home']);
         },
         error: (err) => console.error(err),
      });
   }
}
