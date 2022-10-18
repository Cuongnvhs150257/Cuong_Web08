<template>
        <input type="text" class="item-input"  :class="{ 'item-input-red': !inValue }" :value="modelValue" @input="handleInput" :tabindex="tab" :maxlength="maxlength">

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
        border: 2px solid #50B83C;
        outline: none;
    }
   

    
</style>

<script>

export default {
    props:["modelValue","toolTip","inValue", "tab", "refs", "maxlength"],
    
    setup(props, { emit }){
        
        /**
         * hàm lưu giá trị nhập
         * Nguyễn Văn Cương 26/09/2022
         */
        function handleInput(event){
           emit("update:modelValue",event.target.value);
           
        }
       
        
        return {
        handleInput,
       
    };
    }

    
}
</script>