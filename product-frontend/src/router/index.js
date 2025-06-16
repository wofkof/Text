import { createRouter, createWebHistory } from "vue-router";
import ProductList from "../Views/ProductList.vue";
import ProductForm from "../Views/ProductForm.vue";
import ProductEdit from "../Views/ProductEdit.vue";

const routes = [
  {
    path: "/",
    redirect: "/products",
  },
  {
    path: "/products",
    name: "ProductList",
    component: ProductList,
  },
  {
    path: "/products/create",
    name: "CreateProduct",
    component: ProductForm,
  },
  {
    path: "/products/:id/edit",
    name: "EditProduct",
    component: ProductEdit,
  },
  {
    path: "/products/:id",
    name: "ProductDetail",
    component: ProductList,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
