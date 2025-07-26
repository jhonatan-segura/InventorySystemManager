import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
   InventoryOutputPayload,
   InventoryOutputResponse,
   ProductPayload,
} from '../../models/auth.model';
import { InventoryOutputService } from '../../services/inventory-output.service';
import { Router } from '@angular/router';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';

@Component({
   selector: 'app-inventory-output',
   standalone: true,
   imports: [CommonModule, FormsModule, MatTooltipModule, MatButtonModule],
   templateUrl: './inventory-output.component.html',
})
export class InventoryOutputComponent {
   inventoryOutputs: InventoryOutputResponse[] = [];
   showCreateRow = false;
   editProductId: string | null = null;

   newProduct: ProductPayload = {
      name: '',
      stockQuantity: 0,
      typeOfManufacturingId: 0,
      productStatusId: 0,
   };
   constructor(
      private inventoryOutputService: InventoryOutputService,
      private router: Router
   ) {}

   ngOnInit() {
      this.fetchInventoryOutputs();
   }

   fetchInventoryOutputs() {
      this.inventoryOutputService.getInventoryOutputs().subscribe({
         next: (data: InventoryOutputResponse[]) => {
            this.inventoryOutputs = data;
            console.log(data);
         },
         error: (err) => console.error('Error:', err),
      });
   }

   onCreate() {
      const product: ProductPayload = {
         name: this.newProduct.name ?? '',
         stockQuantity: this.newProduct.stockQuantity ?? 0,
         typeOfManufacturingId: this.newProduct.typeOfManufacturingId ?? 0,
         productStatusId: this.newProduct.productStatusId ?? 0,
      };
      console.log(product);

      this.inventoryOutputService.registerProduct(product).subscribe({
         next: () => {
            this.fetchInventoryOutputs();
            this.showCreateRow = false;
            this.newProduct = {
               name: '',
               stockQuantity: 0,
               typeOfManufacturingId: 0,
               productStatusId: 0,
            };
         },
         error: (err) => console.error('Error al crear producto:', err),
      });
   }

   onEdit(id: string) {
      this.editProductId = id;
   }

   cancelEdit() {
      this.editProductId = null;
   }

   onSaveEdit(id: string, inventoryOutput: InventoryOutputPayload) {
      this.inventoryOutputService.updateProduct(id, inventoryOutput).subscribe({
         next: () => {
            console.log('Producto actualizado');
            this.editProductId = null;
            this.fetchInventoryOutputs();
         },
         error: (err) => console.error('Error al guardar edición:', err),
      });
   }

   onDelete(id: string) {
      this.inventoryOutputService.deleteProduct(id).subscribe({
         next: () => {
            console.log(`Producto ${id} eliminado.`);
            this.fetchInventoryOutputs();
         },
         error: (err) => console.error('Error al eliminar:', err),
      });
   }

   goBack() {
      this.router.navigate(['/home']);
   }
}
