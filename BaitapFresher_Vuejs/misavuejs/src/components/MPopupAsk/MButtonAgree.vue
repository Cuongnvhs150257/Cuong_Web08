<template>
  <button type="button" class="content-top-btn">
    Đồng ý
  </button>
</template>

<style>
.content-top-btn {
  height: 40px;
  padding: 0 24px;
  border: none;
  text-align: center;
  background-color: #29A21A;
  color: #ffff;
  border-radius: 4px;
  cursor: pointer;
  min-width: 100px;
  position: absolute;
  right: 0;
  margin-top: 20px;
  margin-right: 30px;
}
</style>

<script>
export default {
  emit: ["custom-handle-click"],
  methods: {
    
  },
};
</script>