import {createApp} from 'vue'
import Misa from '../src/view/Misa.vue'
import {createRouter, createWebHistory} from "vue-router"
import TheEmployee from '../src/components/Layout/TheContent/TheEmployee/TheEmployee.vue'
import TheProduct from '../src/components/Layout/TheContent/TheProduct/TheProduct.vue'
const routers = [
    {path: "/employee", component:TheEmployee},
    {path: "/product" , component:TheProduct},
]
const router = createRouter({
    history: createWebHistory (),
    routes: routers
})
const app = createApp(Misa);
app.use(router);
app.mount('#app');
