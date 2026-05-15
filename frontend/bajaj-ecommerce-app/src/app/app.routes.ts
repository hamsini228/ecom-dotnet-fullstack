import { Routes } from "@angular/router";
import { EcomHome } from "./features/home/ecom-home/ecom-home";
import { CategoriesList } from "./features/categories/components/categories-list/categories-list";
import { PageNotFound } from "./shared/components/page-not-found/page-not-found";
import { YourCart } from "./features/carts/components/your-cart/your-cart";
import { InvoiceDetail } from "./features/invoices/components/invoice-detail/invoice-detail";
import { tokenCheckerGuard } from "./core/route-guards/token-checker-guard";
import { roleCheckerGuard } from "./core/route-guards/role-checker-guard";
import { RegisterCategory } from "./features/categories/components/register-category/register-category";

const routes: Routes = [
    {
        path: '',
        component: EcomHome,
        title: 'Default Page'
    },
    {
        path: 'home',
        component: EcomHome,
        title: "Home Page"
    },
    {
        path: 'categories',
        component: CategoriesList,
        title: "Categories Page",
        canActivate: [tokenCheckerGuard,roleCheckerGuard],
        data: { roles: ['Admin','Customer'] }
    },
    {
        path: 'categories/register',
        component:RegisterCategory,
        title:'Register Category Page',
        canActivate: [tokenCheckerGuard,roleCheckerGuard],
        data: { roles: ['Admin'] }
    },
    {
        path: 'products',
        loadComponent: () => import('./features/products/components/products-list/products-list')
            .then(m => m.ProductsList),
        title: "Products Page"

    },
    {
        path: 'products/:id',
        loadComponent: () => import('./features/components/product-details/product-details')
            .then(m => m.ProductDetails),
        title: "Product deatils Page"
    },
    {
        path: 'login',
        loadComponent: () => import('./features/security/components/login/login')
            .then(m => m.Login),
        title: "Login Page"
    },
    {
        path: 'cart',
        component: YourCart,
        title: "Your Cart",
        canActivate:[tokenCheckerGuard],
    },
    {
        path: 'invoice/:id',
        component: InvoiceDetail,
        title: "Invoice"
    },

    {
        path: "**",
        component: PageNotFound,
        title: "Error"
    }
]
export default routes;