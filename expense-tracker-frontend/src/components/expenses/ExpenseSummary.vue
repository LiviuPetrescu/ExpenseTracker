<template>
  <div class="mt-4 text-lg font-semibold text-heading dark:text-heading-dark">
    Total: {{ totalAmount }} RON
  </div>
</template>

<script setup>
defineProps({
  totalAmount: {
    type: Number,
    required: true,
  },
})
</script>
