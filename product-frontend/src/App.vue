<template>
  <div>
    <nav>
      <router-link to="/products">商品列表</router-link> |
      <router-link to="/products/create">新增商品</router-link>
    </nav>
    <hr />
    <router-view />
  </div>
</template>

<style scoped>
nav {
  padding: 1rem;
}
</style>
