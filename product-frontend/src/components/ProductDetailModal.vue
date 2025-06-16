<template>
  <div v-if="visible" class="modal-overlay" @click.self="onClose">
    <div class="modal">
      <h2>商品詳細資訊</h2>
      <p><strong>編號：</strong>{{ product.id }}</p>
      <p><strong>名稱：</strong>{{ product.name }}</p>
      <p><strong>價格：</strong>{{ product.price }}</p>
      <p><strong>描述:</strong>{{ product.description }}</p>
      <button @click="onClose">關閉</button>
    </div>
  </div>
</template>

<script setup>
defineProps({
  visible: Boolean,
  product: {
    type: Object,
    required: true,
  },
});

const emit = defineEmits(["close"]);

const onClose = () => {
  emit("close");
};
</script>

<style scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.4);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 999;
}
.modal {
  background: black;
  padding: 20px;
  border-radius: 8px;
  width: 300px;
}
.modal button {
  margin-top: 10px;
}
</style>
