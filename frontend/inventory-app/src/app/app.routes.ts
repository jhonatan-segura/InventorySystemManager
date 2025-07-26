import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'home',
    loadComponent: () =>
      import('./pages/home/home.component').then((m) => m.HomeComponent),
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./pages/login/login.component').then((m) => m.LoginComponent),
  },
  {
    path: 'register',
    loadComponent: () =>
      import('./pages/register/register.component').then((m) => m.RegisterComponent),
  },
  {
    path: 'product-list',
    loadComponent: () =>
      import('./pages/product-list/product-list.component').then((m) => m.ProductListComponent),
  },
  {
    path: 'inventory-output',
    loadComponent: () =>
      import('./pages/inventory-output/inventory-output.component').then((m) => m.InventoryOutputComponent),
  },
];
