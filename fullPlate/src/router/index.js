import Vue from 'vue';
import Router from 'vue-router';

import Login from '@/views/Login';
import Page from '@/views/Page';
import FridayOrders from '@/views/orders/FridayOrders';
import NotFound from '@/views/NotFound';
import RestaurantsTable from '@/views/restaurants/RestaurantsTable';
import Index from '@/views/Index';
import DishesTable from '@/views/dishes/DishesTable';

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: '/login',
      name: 'Login',
      component: Login
    },
    {
      path: '/',
      name: 'index',
      component: Page,
      children: [
        {
          path: '',
          component: Index
        },
        {
          path: 'friday-orders',
          name: 'Friday orders',
          component: FridayOrders
          // beforeEnter: (to, from, next) => {
          //   console.log(to)
          //   console.log(from)
          //   next('/restaurants')
          // }
        },
        {
          path: 'restaurants',
          name: 'Restaurants',
          component: RestaurantsTable
        },
        {
          path: 'restaurants/:id/dishes',
          name: 'Restaurant',
          component: DishesTable
        }
      ]
    },
    {
      path: '*',
      component: Page,
      children: [
        {
          path: '*',
          component: NotFound
        }
      ]
    }
  ]
});
