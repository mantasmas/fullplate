import Vue from 'vue';
import Router from 'vue-router';

import store from '../store/index';
import userRoles from '../utils/enums/userRoles';
import Login from '@/views/Login';
import Page from '@/views/Page';
import NewOrder from '@/views/orders/NewOrder';
import NotFound from '@/views/NotFound';
import RestaurantsTable from '@/views/restaurants/RestaurantsTable';
import Index from '@/views/Index';
import DishesTable from '@/views/dishes/DishesTable';
import FridayLunches from '@/views/fridayLunches/FridayLunches';
import NewLunch from '@/views/fridayLunches/NewLunch';
import UsersList from '@/views/userManagement/UserList';
import RegisterUser from '@/views/userManagement/RegisterUser';
import UserLunchesList from '@/views/orders/UserLunchesList';

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
          path: 'friday-lunches',
          name: 'Friday Lunches',
          component: FridayLunches,
          beforeEnter: (to, from, next) => {
            if (store.state.user.role !== userRoles.ADMIN) {
              next('/not-found');
            } else {
              next();
            }
          }
        },
        {
          path: 'friday-lunches/new-lunch',
          name: 'New Lunch',
          component: NewLunch,
          beforeEnter: (to, from, next) => {
            if (store.state.user.role !== userRoles.ADMIN) {
              next('/not-found');
            } else {
              next();
            }
          }
        },
        {
          path: 'restaurants',
          name: 'Restaurants',
          component: RestaurantsTable,
          beforeEnter: (to, from, next) => {
            if (store.state.user.role !== userRoles.ADMIN) {
              next('/not-found');
            } else {
              next();
            }
          }
        },
        {
          path: 'restaurants/:id/dishes',
          name: 'Restaurant',
          component: DishesTable,
          beforeEnter: (to, from, next) => {
            if (store.state.user.role !== userRoles.ADMIN) {
              next('/not-found');
            } else {
              next();
            }
          }
        },
        {
          path: 'restaurants/:id/dishes',
          name: 'Restaurant',
          component: DishesTable,
          beforeEnter: (to, from, next) => {
            if (store.state.user.role !== userRoles.ADMIN) {
              next('/not-found');
            } else {
              next();
            }
          }
        },
        {
          path: 'register-user',
          name: 'RegisterUser',
          component: RegisterUser,
          beforeEnter: (to, from, next) => {
            if (store.state.user.role !== userRoles.ADMIN) {
              next('/not-found');
            } else {
              next();
            }
          }
        },
        {
          path: 'users',
          name: 'UsersList',
          component: UsersList,
          beforeEnter: (to, from, next) => {
            if (store.state.user.role !== userRoles.ADMIN) {
              next('/not-found');
            } else {
              next();
            }
          }
        },
        {
          path: 'available-lunches',
          name: 'AvailableLunches',
          component: UserLunchesList,
          beforeEnter: (to, from, next) => {
            if (store.state.user.role !== userRoles.EMPLOYEE) {
              next('/not-found');
            } else {
              next();
            }
          }
        },
        {
          path: 'available-lunches/:lunchId/order',
          name: 'NewOrder',
          component: NewOrder,
          beforeEnter: (to, from, next) => {
            if (store.state.user.role !== userRoles.EMPLOYEE) {
              next('/not-found');
            } else {
              next();
            }
          }
        }
      ],
      beforeEnter: (to, from, next) => {
        if (store.state.user.role === '') {
          next('/login');
        } else {
          next();
        }
      }
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
