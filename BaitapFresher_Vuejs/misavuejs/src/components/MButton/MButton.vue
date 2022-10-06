<template>
  <button type="button" class="content-top-btn" @click="handleOpenPopup">
    Thêm nhân viên
  </button>
</template>

<style>
.content-top-btn {
  height: 40px;
  padding: 0 24px;
  border: none;
  text-align: center;
  background-color: #2CA01C;
  color: #ffff;
  border-radius: 4px;
  cursor: pointer;
  min-width: 100px;
  position: absolute;
  right: 0;
  margin-top: 20px;
  margin-right: 30px;
}.content-top-btn:hover{
  background-color: #35BF22;
}
</style>

<script>
export default {
  emit: ["custom-handle-click"],
  methods: {

    //hàm mở popup thêm nhân viên
    handleOpenPopup() {
      this.$emit("custom-handle-click"); //mở popup thêm nhân viên
    },
  },
};
</script>