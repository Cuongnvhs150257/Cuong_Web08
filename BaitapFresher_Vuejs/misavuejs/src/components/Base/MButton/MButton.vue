<template>
  <button type="button" class="content-top-btn-content" @click="handleOpenPopup">
    {{text}}
  </button>
  <span v-if="toolTip" class="tool-tip">
            {{ toolTip }}
 </span>
</template>

<style>
.content-top-btn-content {
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
  font-weight: 700;
}.content-top-btn-content:hover{
  background-color: #35BF22;
}
</style>

<script>
export default {
  emit: ["custom-handle-click"],
  props:{
      text: String, 
  },
  methods: {
 
    //hàm mở popup thêm nhân viên (Nguyễn Văn Cương 05/10/2022)
    handleOpenPopup() {
      this.$emit("custom-handle-click"); //mở popup thêm nhân viên
    },
  },
};
</script>