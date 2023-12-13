import os
import subprocess


def upscale_images(input_directory, ignore_dirs):
    # Walk through the directory
    for root, dirs, files in os.walk(input_directory):
        # Modify the 'dirs' list in place to exclude ignored directories
        dirs[:] = [d for d in dirs if os.path.join(root, d) not in ignore_dirs]

        for file in files:
            if file.endswith(".png"):
                file_path = os.path.join(root, file)
                output_file = f"{os.path.splitext(file_path)[0]}.bmp"

                # Upscale the image using ffmpeg
                command = [
                    "ffmpeg",
                    "-i",
                    file_path,
                    "-update",
                    "1",
                    "-frames:v",
                    "1",
                    "-vf",
                    "scale=48:48:flags=neighbor",
                    output_file,
                    "-y",
                ]
                subprocess.run(command)


if __name__ == "__main__":
    # Set your input directory and directories to ignore
    script_dir = os.path.dirname(os.path.realpath(__file__))
    input_directory = os.path.join(script_dir, "textures")
    ignore_dirs = [
        os.path.join(input_directory, "test"),
        os.path.join(input_directory, "ignore_dir2"),
    ]

    upscale_images(input_directory, ignore_dirs)
