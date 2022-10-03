<template>
    <input
            type="text"
            class="toolbar-input"
            placeholder="Tìm theo mã, tên nhân viên"
            :value="InputWhere" @input="handleInput"
          />
</template>

<script>
export default {
    
    setup(props, { emit }){
        
        //hàm lưu giá trị nhập
        function handleInput(event){
           emit("InputWhere",event.target.value);
           
        }
       
        
        return {
        handleInput,
       
    };
    }
}
</script>

<style>
    .toolbar-input {
  height: 30px;
  width: 200px;
  border-width: 1px;
  border-radius: 4px;
  border-color: #bbbbbb;
  color: #000;
  padding: 0 16px;
  border-style: solid;
  outline: unset;
}
.toolbar-input:focus {
  border-color: #019160;
}
</style>