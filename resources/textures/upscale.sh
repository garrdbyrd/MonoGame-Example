#!/bin/bash

# Directory containing the textures
input_directory="$(dirname)"

# Directories to ignore during the search
ignore_dirs=("test" "ignore_dir2")

# Build the find command, ignoring specified directories
find_command="find $input_directory -type d"
for dir in "${ignore_dirs[@]}"; do
    find_command+=" \( -path $input_directory/$dir -prune \) -o"
done
find_command+=" -type f -name '*.png' -print"

# Execute the find command and upscale each .png file
eval "$find_command" | while read -r file; do
    # Determine the directory of the current file
    file_dir=$(dirname "$file")

    # Generate the output filename with .bmp extension in the same directory
    output_file="$file_dir/$(basename "${file%.*}").bmp"
    
    # Upscale the image to 48x48 using ffmpeg and save as .bmp
    ffmpeg -i "$file" -frames:v 1 -vf "scale=48:48:flags=neighbor" "$output_file" -y
done
