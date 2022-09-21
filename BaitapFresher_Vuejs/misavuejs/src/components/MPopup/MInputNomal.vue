<template>
    <div class="data-input">
         <input type="text" class="item-input" :value="modelValue" @input="handleInput">
        <span v-if="toolTip" class="tool-tip">
        {{ toolTip }}
    </span>
    </div>
   
    
</template>
<style>
    .data-input{
        position: relative;
    }
    .item-input{
        height: 28px;
        width: 100%;
        border: 1px solid black;
        margin-top: 5px;
        outline: none;
        padding-left: 10px; 
    }
    .item-input:focus{
        border: 2px solid #019160;
        outline: none;
    }
   

    .tool-tip {
        position: absolute;
        left: 30px;
        top: 20px;
        background-color: #505050;
        border-radius: 2px;
        padding: 2px 4px;
        z-index: 3;
        text-align: center;
        color: #fff;
        visibility: hidden;
        
    }
    .data-input:hover .tool-tip {
        visibility: visible;
        opacity: 1;
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