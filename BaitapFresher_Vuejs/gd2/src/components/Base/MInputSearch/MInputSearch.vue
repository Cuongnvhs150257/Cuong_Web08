<template>
  <div class="toolbar-search">
    <input
      type="text"
      class="toolbar-input-search"
      :placeholder="placeholder"
      :value="InputWhere"
      @input="handleInput"
      :disabled="disable"
      :style="style"
    />
    <button :class="iconsearch"></button>
  </div>
</template>

<script>
export default {
  props:{
    placeholder: String,
    style: String,
    iconsearch: String,
    disable: Boolean,
  },
  methods:{
        /**
         * hàm cập nhật giá trị tìm kiếm
         * Nguyễn Văn Cương 15/09/2022
         */
    handleInput(event) {
      this.$emit("InputWhere", event.target.value);
    }
  },data(){
    return{

    }
  }
}

</script>

<style>
:root {
  --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
}
.toolbar-search {
  position: relative;
}
.toolbar-input-search {
  height: 30px;
  width: 200px;
  border-width: 1px;
  border-radius: 4px;
  border-color: #bbbbbb;
  color: #000;
  padding: 0 8px;
  border-style: solid;
  outline: unset;
  font-style: italic;
  font-size: 12px;
}
.toolbar-input-search:focus {
  border: 1px solid #50B83C;
}
.icon-search {
  width: 28px;
  height: 28px;
  position: absolute;
  right: 5px;
  border: none;
  background-color: #fff;
  top: 2px;
}
.icon-search {
  background-image: var(--icon);
  background-position: -425px -30px;
  background-repeat: no-repeat;
}.icon-search.b{
    right: 10px;
}
.icon-search.c{
    right: 3px;
}
</style>