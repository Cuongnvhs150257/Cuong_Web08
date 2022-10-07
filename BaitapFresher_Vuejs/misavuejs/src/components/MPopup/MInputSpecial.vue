<template>
        <input type="text" class="item-input" :value="modelValue" @input="handleInput">

</template>
<style>

    .item-input{
        height: 28px;
        width: 100%;
        border: 1px solid black;
        margin-top: 5px;
        outline: none;
        padding-left: 10px; 
        border-radius: 3px;
    }
    .item-input:focus{
        border: 2px solid #019160;
        outline: none;
    }
   

    
</style>

<script>

export default {
    
    props:["modelValue","toolTip"],
    
    setup(props, { emit }){
        
        //hàm lưu giá trị nhập
        function handleInput(event){
           emit("update:modelValue",event.target.value);
           
        }
       
        
        return {
        handleInput,
       
    };
    }

    
}
</script>