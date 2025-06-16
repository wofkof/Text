<template>
  <div class="container"> 
    <h1>新增商品</h1>
    <form @submit.prevent="submitForm">
        <div>
            <label for="name">名稱：</label>
            <input id="name" v-model="product.name" required />
        </div>
        <div>
            <label for="price">價格：</label>
            <input id="price" v-model="product.price" required />
        </div>
        <button type="submit">送出</button>

        <p v-if="successMessage" style="color: green;">{{ successMessage }}</p>
    </form>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { createProduct } from "../api/productApi"; 

const router = useRouter();
const product = ref({
    name: "",
    price: 0
})
const successMessage = ref("");
const submitForm = async () => { 
    try { 
        await createProduct(product.value);
        successMessage.value = "新增成功，3秒後返回列表..."
        setTimeout(() => { 
            router.push("/products");
        }, 3000)
    }catch (error) {
        console.error("新增失敗", error);
    }
}

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