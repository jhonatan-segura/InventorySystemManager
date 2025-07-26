import { Component, inject, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
   InventoryOutputPayload,
   ProductPayload,
   ProductResponse,
   ProductStatusResponse,
   TypeOfManufacturingResponse,
} from '../../models/auth.model';
import { ProductService } from '../../services/product.service';
import { ProductStatusService } from '../../services/product-status.service';
import { TypeOfManufacturingService } from '../../services/type-of-manufacturing.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { InventoryOutputDialogComponent } from '../../components/inventory-output-dialog/inventory-output-dialog.component';
import { Router } from '@angular/router';

@Component({
   selector: 'app-product-list',
   standalone: true,
   imports: [
      CommonModule,
      FormsModule,
      MatDialogModule,
      MatButtonModule,
      MatTooltipModule,
   ],
   templateUrl: './product-list.component.html',
   encapsulation: ViewEncapsulation.None,
})
export class ProductListComponent {
   readonly dialog = inject(MatDialog);
   products: ProductResponse[] = [];
   productStatuses: ProductStatusResponse[] = [];
   typesOfManufacturing: TypeOfManufacturingResponse[] = [];
   showCreateRow = false;
   editProductId: string | null = null;

   newProduct: ProductPayload = {
      name: '',
      stockQuantity: 0,
      typeOfManufacturingId: 0,
      productStatusId: 0,
   };
   constructor(
      private productService: ProductService,
      private productStatusService: ProductStatusService,
      private typeOfManufacturing: TypeOfManufacturingService,
      private router: Router
   ) {}

   ngOnInit() {
      this.fetchProducts();
      this.fetchProductStatuses();
      this.fetchTypesOfManufacturing();
   }

   fetchProducts() {
      this.productService.getProducts().subscribe({
         next: (data: ProductResponse[]) => {
            this.products = data;
            console.log(data);
         },
         error: (err) => console.error('Error:', err),
      });
   }

   fetchProductStatuses() {
      this.productStatusService.getProductStatuses().subscribe({
         next: (data: ProductStatusResponse[]) => {
            this.productStatuses = data;
         },
         error: (err) => console.error('Error:', err),
      });
   }

   fetchTypesOfManufacturing() {
      this.typeOfManufacturing.getTypesOfManufacturing().subscribe({
         next: (data: TypeOfManufacturingResponse[]) => {
            this.typesOfManufacturing = data;
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

      this.productService.registerProduct(product).subscribe({
         next: () => {
            this.fetchProducts();
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

   onSaveEdit(id: string, product: ProductPayload) {
      this.productService.updateProduct(id, product).subscribe({
         next: () => {
            console.log('Producto actualizado');
            this.editProductId = null;
            this.fetchProducts();
         },
         error: (err) => console.error('Error al guardar edición:', err),
      });
   }

   onDelete(id: string) {
      this.productService.deleteProduct(id).subscribe({
         next: () => {
            console.log(`Producto ${id} marcado como defectuoso.`);
            this.fetchProducts();
         },
         error: (err) => console.error('Error al marcar como defectuoso:', err),
      });
   }

   onMoveProduct(productId: string) {
      const dialogRef = this.dialog.open(InventoryOutputDialogComponent, {
         data: productId,
         width: '400px',
      });

      dialogRef.afterClosed().subscribe((result) => {
         if (result) {
            const body: InventoryOutputPayload = {
               productId: productId,
               stockQuantity: result.quantity,
               reason: result.reason,
            };
            this.productService.moveToOutput(body).subscribe({
               next: () => {
                  console.log(`Producto ${productId} movido a la salida.`);
                  this.fetchProducts();
               },
               error: (err) =>
                  console.error('Error al marcar como defectuoso:', err),
            });
         }
      });
   }

   onMarkDefective(id: string) {
      this.productService.setProductAsFaulty(id).subscribe({
         next: () => {
            console.log(`Producto ${id} marcado como defectuoso.`);
            this.fetchProducts();
         },
         error: (err) => console.error('Error al marcar como defectuoso:', err),
      });
   }

   goBack() {
      this.router.navigate(['/home']);
   }
}
