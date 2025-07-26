import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { RegisterPayload } from '../../models/auth.model';
import { UsersService } from '../../services/users.service';

@Component({
   selector: 'app-register',
   standalone: true,
   imports: [CommonModule, RouterModule, FormsModule],
   templateUrl: './register.component.html',
})
export class RegisterComponent {
   firstName = '';
   lastName = '';
   email = '';
   password = '';
   constructor(private userService: UsersService) {}

   onRegister() {
      const credentials: RegisterPayload = {
         firstName: this.firstName,
         lastName: this.lastName,
         email: this.email,
         password: this.password,
      };

      this.userService.register(credentials).subscribe({
         next: (res) => {
            console.log(res);
         },
         error: (err) => console.error(err),
      });
   }
}
