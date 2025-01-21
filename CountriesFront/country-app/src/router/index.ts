import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import CountryGrid from "../components/CountryGrid.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "home",
    component: CountryGrid,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
