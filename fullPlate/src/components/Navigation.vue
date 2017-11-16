<template>
  <div class="page-sub-header">
    <div class="page-frame">
      <nav class="site-navigation extended">
        <router-link to="/late-requests" class="nav-item">Late Requests</router-link>
        <router-link to="/friday-orders" class="nav-item">Friday orders</router-link>
        <router-link to="/totals" class="nav-item">Totals</router-link>
        <router-link to="/restaurants" class="nav-item">Restaurants</router-link>
      </nav>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'Navigation'
  };
</script>
