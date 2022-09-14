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
    }
    .item-input:focus{
        border: 2px solid #019160;
        outline: none;
        }
</style>

<script>
export default {
    
    props:["modelValue"],
    methods:{
        handleInput(event){
           this.$emit("update:modelValue",event.target.value);
        }
    }
        
    
}
</script>