<template>
  <div class="container">
    <h1>編輯商品</h1>
    <form v-if="product" @submit.prevent="productEdit(product.id)">
      <div>
        <label for="name">名稱:</label>
        <input id="name" v-model="product.name" required />
      </div>
      <div>
        <label for="price">價格:</label>
        <input id="price" v-model="product.price" required />
      </div>
      <div>
        <button type="submit">送出</button>
      </div>
    </form>

    <p v-if="successMessage" style="color: green">{{ successMessage }}</p>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { getProductById, updateProduct } from "../api/productApi";
import { useRoute, useRouter } from "vue-router";

const router = useRouter();
const route = useRoute();

const product = ref(null);
const successMessage = ref("");

const loadProductId = async () => {
  try {
    const response = await getProductById(route.params.id);
    product.value = response.data;
  } catch (error) {
    console.error("載入商品失敗", error);
  }
};

const productEdit = async () => {
  try {
    await updateProduct(product.value.id, product.value);
    successMessage.value = "編輯成功，3秒後返回列表...";
    setTimeout(() => {
      router.push("/products");
    }, 3000);
  } catch (error) {
    console.error("編輯失敗", error);
  }
};

onMounted(() => {
  loadProductId();
});
</script>

<style scoped>
.container {
  max-width: 600px;
  margin: auto;
}
form > div {
  margin-bottom: 1rem;
}
input {
  padding: 5px;
  width: 100%;
}
button {
  padding: 5px 10px;
}
</style>
