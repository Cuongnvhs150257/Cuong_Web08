import { createApp } from 'vue'
import App from './App.vue'
import {createRouter, createWebHistory} from "vue-router"
import TheProduct from '../src/view/ProductPage/TheProduct.vue'
import TheEmployee from './view/EmployeePage/EmployeePopup/TheEmployee.vue'
import TheWarehouse from '../src/view/WarehousePage/TheWarehouse.vue'
import TheUnit from '../src/view/UnitPage/TheUnit.vue'
const routers = [
    {path: "/Theemployee" , component:TheEmployee,},
    {path: "/Theproduct" , component:TheProduct,},
    {path: "/Thewarehouse", component: TheWarehouse,},
    {path: "/Theunit", component: TheUnit,},
]
const router = createRouter({
    history: createWebHistory (),
    routes: routers
})


const app = createApp(App)
app.use(router);
app.mount('#app');
 