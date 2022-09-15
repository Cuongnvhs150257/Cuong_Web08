<template>
    <input type="text" class="item-input" :class="{'item-input-red':!inValue}" :value="modelValue" @input="handleInput" @checkNull="inNull">
</template>
<style>
    .item-input{
        height: 28px;
        width: 100%;
        border: 1px solid black;
        margin-top: 5px;
        outline: none;
        padding-left: 10px; 
    }.item-input-red{
        border: 1px solid #ff0000;
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
           
        },
        inNull(){
            this.inValid = false;
        }
    },
    data(){
        return{
            inValid: true
        }
    }
        
    
}
</script>