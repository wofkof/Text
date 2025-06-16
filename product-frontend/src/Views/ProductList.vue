<template>
  <div class="container">
    <h1>商品清單</h1>
    <table border="1" cellpadding="8" cellspacing="0">
      <thead>
        <tr>
          <th>編號</th>
          <th>名稱</th>
          <th>價格</th>
          <th>操作</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="product in products" :key="product.id">
          <td>{{ product.id }}</td>
          <td>{{ product.name }}</td>
          <td>{{ product.price }}</td>
          <td>
            <button @click="productDetail(product.id)">查看</button>
            <button @click="editProduct(product.id)">編輯</button>
            <button @click="productDelete(product.id)">刪除</button>
          </td>
        </tr>
      </tbody>
    </table>
    <ProductDetailModal :visible="showModal" :product="selectedProduct" @close="showModal = false" />
  </div>
</template>

<script setup>
import ProductDetailModal from '../components/ProductDetailModal.vue';
import { ref, onMounted } from 'vue';
import { getAllProducts, deleteProduct, getProductById } from '../api/productApi';
import { useRouter } from 'vue-router';
const products = ref([]);
const router = useRouter();
const showModal = ref(false)
const selectedProduct = ref({})

const editProduct = (id) => {
  router.push(`/products/${id}/edit`);
}

const loadProducts = async () => {
  try {
    const response = await getAllProducts();
    console.log("全部商品：", response.data);
    products.value = response.data;
  } catch (error) {
    console.error("載入失敗", error);
  }
};

const productDetail = async(id) => {
  try {
    const response = await getProductById(id);
    selectedProduct.value = response.data
    showModal.value = true
    console.log("單一商品", response.data);
  } catch (error) {
    console.error("載入失敗", error);
  }
}

const productDelete = async (id) => { 
  if (!confirm("確定刪除這個商品嗎？")) return;
  try {
    await deleteProduct(id);
    products.value = products.value.filter(p => p.id !== id);
  } catch (error) {
    console.error("刪除失敗", error);
  }
}

onMounted(() => {
  loadProducts();
});
</script>

<style scoped>
.container {
  max-width: 800px;
  margin: auto;
}
table {
  width: 100%;
}
</style>

