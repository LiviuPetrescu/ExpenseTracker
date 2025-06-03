<template>
  <form
    @submit.prevent="handleSubmit"
    class="mb-4 p-3 sm:p-5 border border-border rounded-lg shadow bg-background-soft dark:bg-background-soft-dark space-y-3 max-w-xl mx-auto text-sm"
  >
    <!-- Form Grid -->
    <div class="grid grid-cols-1 sm:grid-cols-2 gap-3">
      <!-- Category -->
      <div>
        <label class="block font-semibold text-heading mb-1">Category</label>
        <select
          v-model="form.category"
          required
          class="w-full border border-border rounded px-2 py-1.5 focus:outline-none focus:ring-2 focus:ring-heading"
        >
          <option value="" disabled>Select category</option>
          <option v-for="cat in categories" :key="cat" :value="cat">{{ cat }}</option>
        </select>
      </div>

      <!-- Title -->
      <div>
        <label class="block font-semibold text-heading mb-1">Title</label>
        <input
          v-model="form.title"
          type="text"
          required
          class="w-full border border-border rounded px-2 py-1.5 focus:outline-none focus:ring-2 focus:ring-heading"
        />
      </div>

      <!-- Amount -->
      <div>
        <label class="block font-semibold text-heading mb-1">Amount (RON)</label>
        <input
          v-model.number="form.amount"
          type="number"
          step="0.01"
          required
          class="w-full border border-border rounded px-2 py-1.5 focus:outline-none focus:ring-2 focus:ring-heading"
        />
      </div>

      <!-- Date (FlatPickr) -->
      <div>
        <label class="block font-semibold text-heading mb-1">Date</label>
        <FlatPickr
          v-model="form.date"
          placeholder="Select Date"
          class="w-full text-sm border border-border rounded px-2 py-1.5 focus:outline-none focus:ring-2 focus:ring-heading"
        />
      </div>
    </div>

    <!-- Notes -->
    <div>
      <label class="block font-semibold text-heading mb-1">Notes</label>
      <textarea
        v-model="form.notes"
        rows="2"
        class="w-full border border-border rounded px-2 py-1.5 focus:outline-none focus:ring-2 focus:ring-heading resize-none"
      ></textarea>
    </div>

    <!-- Actions -->
    <div class="flex flex-col sm:flex-row sm:space-x-3 space-y-2 sm:space-y-0 pt-1">
      <button
        type="submit"
        class="bg-logo-blue text-white px-4 py-2 rounded hover:bg-logo-blue-dark transition focus:outline-none focus:ring-2 focus:ring-logo-blue"
      >
        {{ isEditMode ? 'Update Expense' : 'Add Expense' }}
      </button>
      <button
        v-if="isEditMode"
        type="button"
        @click="cancelEdit"
        class="text-gray-500 hover:underline"
      >
        Cancel
      </button>
    </div>
  </form>
</template>


<script setup>
import { ref, watch, onMounted } from 'vue'
import FlatPickr from 'vue-flatpickr-component'
import 'flatpickr/dist/flatpickr.css'
import axios from 'axios'

const emit = defineEmits(['expense-added', 'edit-finished'])
const props = defineProps({ editData: Object })

const emptyForm = {
  id: null,
  title: '',
  amount: null,
  category: '',
  date: '',
  notes: ''
}

const form = ref({ ...emptyForm })
const isEditMode = ref(false)
const categories = ref([])

const fetchCategories = async () => {
  try {
    const { data } = await axios.get('http://localhost:5079/api/expenses/categories')
    categories.value = data
  } catch (e) {
    console.error('Failed to fetch categories:', e)
  }
}

onMounted(fetchCategories)

watch(
  () => props.editData,
  (newVal) => {
    if (newVal) {
      // ensure yyyy-MM-dd format
      const [day, month, year] = newVal.date.split('-')
      form.value = {
        ...newVal,
        date: `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`
      }
      isEditMode.value = true
    }
  },
  { immediate: true }
)

const handleSubmit = async () => {
  try {
    // Format date if needed
    let formattedDate = form.value.date
    if (formattedDate.includes('-') && formattedDate.split('-')[0].length === 2) {
      const [day, month, year] = formattedDate.split('-')
      formattedDate = `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`
    }

    const payload = { ...form.value, date: formattedDate }

    if (isEditMode.value && form.value.id) {
      await axios.put(`http://localhost:5079/api/expenses/${form.value.id}`, payload)
      emit('edit-finished')
    } else {
      await axios.post('http://localhost:5079/api/expenses', payload)
    }

    emit('expense-added')
    resetForm()
  } catch (e) {
    console.error('Failed to submit expense:', e)
    alert('Failed to save expense.')
  }
}

const cancelEdit = () => {
  resetForm()
  emit('edit-finished')
}

const resetForm = () => {
  form.value = { ...emptyForm }
  isEditMode.value = false
}
</script>
