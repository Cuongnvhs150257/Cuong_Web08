<template>
  <div class="toolbar-search">
    <input
      type="text"
      class="toolbar-input-search"
      placeholder="Tìm theo mã, tên nhân viên"
      :value="InputWhere"
      @input="handleInput"
    />
    <button class="icon-search"></button>
  </div>
</template>

<script>
export default {
  methods:{
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
  border: 2px solid #019160;
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
}
</style>