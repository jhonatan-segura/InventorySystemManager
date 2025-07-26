import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { UsersService } from '../../services/users.service';

@Component({
   selector: 'app-home',
   standalone: true,
   imports: [CommonModule, RouterModule],
   templateUrl: './home.component.html',
})
export class HomeComponent {
   constructor(private api: UsersService, private router: Router) {}

   onOpenInventory() {
      this.router.navigate(['/product-list']);
   }

   onOpenInventoryOutput() {
      this.router.navigate(['/inventory-output']);
   }
}
